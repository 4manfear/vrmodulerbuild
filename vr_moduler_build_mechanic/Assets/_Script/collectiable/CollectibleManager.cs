using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    public int rockCount = 0;
    public int coalCount = 0;

    private void Update()
    {
        // Update the text to show the current rock count
        tmp.text = "Rocks: " + rockCount.ToString() + "\nCoal: " + coalCount.ToString();
    }

    // Collect a rock
    public void CollectRock()
    {
        rockCount++;
        Debug.Log("Collected a Rock! Total Rocks: " + rockCount);
        UpdateRockText();
    }

    // Collect coal
    public void CollectCoal()
    {
        coalCount++;
        Debug.Log("Collected Coal! Total Coal: " + coalCount);
        UpdateRockText();
    }

    // Update the text based on the rock count
    private void UpdateRockText()
    {
        tmp.text = "Rocks: " + rockCount.ToString() + "\nCoal: " + coalCount.ToString();
    }

    // Checks if the player has any rocks
    public bool HasRocks()
    {
        return rockCount > 0;
    }
}
