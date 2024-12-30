using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class modulerbuilder : MonoBehaviour
{
    [SerializeField] private InputActionReference _rightHandPrimaryButton;   // Input for cycling between buildables
    [SerializeField] private InputActionReference _rightHandSecondaryButton; // Input for placing the object

    [SerializeField] private Transform playerTransform; // Reference to the player transform
    [SerializeField] private GameObject cube1, cube2, cube3; // Buildable prefabs

    [SerializeField] private LayerMask placement_Layer; // Layer mask for valid placement surfaces
    [SerializeField] private float preview_offset = 0.5f; // Offset to place objects above the ground

    [SerializeField] private Transform rightHandTransform; // The VR hand used for raycasting (controller)
    [SerializeField] private float rayLength = 10f; // Max raycast distance

    private GameObject[] buildablePrefabs; // Array of buildable prefabs
    private int currentPrefabIndex = -1;   // Index to track current prefab
    private GameObject currentBuildablePrefab; // Current prefab to place
    private GameObject previewGameObject; // Preview object for placement

    private CollectibleManager collectibleManager; // Reference to CollectibleManager

    private void Start()
    {
        // Initialize the buildable prefab array
        buildablePrefabs = new GameObject[] { cube1, cube2, cube3 };

        // Find and reference the CollectibleManager component (assumes it's on the player or in the scene)
        collectibleManager = FindObjectOfType<CollectibleManager>();
    }

    private void Update()
    {
        // Only allow building if the player has at least one rock
        if (collectibleManager != null && collectibleManager.HasRocks())
        {
            // Handle cycling between buildables with the primary button
            if (_rightHandPrimaryButton.action.triggered)
            {
                CycleBuildable();
            }

            // Handle placement with the secondary button
            if (currentBuildablePrefab != null && _rightHandSecondaryButton.action.triggered)
            {
                PlaceBuildable();
            }

            // Handle preview and placement if a prefab is selected
            if (currentBuildablePrefab != null)
            {
                HandlePreview();
            }
        }
    }

    // Cycles through the buildable objects
    private void CycleBuildable()
    {
        currentPrefabIndex++;
        if (currentPrefabIndex >= buildablePrefabs.Length)
        {
            currentPrefabIndex = 0; // Loop back to the first prefab
        }

        SetBuildable(buildablePrefabs[currentPrefabIndex]);
    }

    // Set the current buildable prefab
    private void SetBuildable(GameObject buildablePrefab)
    {
        currentBuildablePrefab = buildablePrefab;
        if (previewGameObject != null)
        {
            Destroy(previewGameObject); // Destroy old preview if exists
        }
        CreatePreviewObject();
    }

    // Create a preview object to show where the buildable will be placed
    private void CreatePreviewObject()
    {
        if (currentBuildablePrefab != null)
        {
            previewGameObject = Instantiate(currentBuildablePrefab);
            previewGameObject.GetComponent<Collider>().enabled = false; // Disable collider for preview
        }
    }

    // Handle preview placement using a ray from the VR controller's hand
    private void HandlePreview()
    {
        RaycastHit hit;

        // Cast a ray from the right-hand controller (or VR hand)
        if (Physics.Raycast(rightHandTransform.position, rightHandTransform.forward, out hit, rayLength, placement_Layer))
        {
            Vector3 placementPosition = hit.point + Vector3.up * preview_offset;
            if (previewGameObject != null)
            {
                previewGameObject.transform.position = placementPosition;
                previewGameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }

    // Place the buildable object at the preview location
    private void PlaceBuildable()
    {
        if (previewGameObject != null)
        {
            // Get the player's forward direction
            Vector3 forwardDirection = playerTransform.forward;

            // Instantiate the object at the preview position with the player's forward direction
            Instantiate(currentBuildablePrefab, previewGameObject.transform.position, Quaternion.LookRotation(forwardDirection));
            Destroy(previewGameObject); // Remove the preview object after placement
            collectibleManager.rockCount--;
        }
    }


}
