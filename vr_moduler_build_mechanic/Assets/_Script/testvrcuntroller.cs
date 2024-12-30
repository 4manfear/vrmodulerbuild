using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testvrcuntroller : MonoBehaviour
{
    [Header("Right Hand Inputs")]
    [SerializeField] private InputActionReference _rightHandTriggerButton;
    [SerializeField] private InputActionReference _rightHandGrabButton;
    [SerializeField] private InputActionReference _rightHandPrimaryButton;
    [SerializeField] private InputActionReference _rightHandSecondaryButton;


    [Header("Left Hand Inputs")]
    [SerializeField] private InputActionReference _leftHandTriggerButton;
    [SerializeField] private InputActionReference _leftHandGrabButton;
    [SerializeField] private InputActionReference _leftHandPrimaryButton;
    [SerializeField] private InputActionReference _leftHandSecondaryButton;


    public bool inhand;

    public GameObject righthand;
    public Transform leftHand;

    public GameObject pickax;

    float distanceoflefthandandax;
    float distanceofrighthandandax;

    public float mindistancetopickup;

    private void Awake()
    {
        pickax = GameObject.FindGameObjectWithTag("pickax");
        
    }

    private void Update()
    {
        pikaxandhanddistance();

        if(_rightHandPrimaryButton.action.ReadValue<float>()>0.05)
        {
            Debug.Log("pressed");
        }

        if (_leftHandTriggerButton.action.ReadValue<float>() > 0.5f)
        {
            
        }

        if (_rightHandTriggerButton.action.ReadValue<float>() > 0.05)
        {
            pickaxintorighthand();
        } 
        if (_rightHandTriggerButton.action.ReadValue<float>() < 0.05)
        {
            Dropingthepickax();
        }


       
    }

    void pikaxandhanddistance()
    {
         distanceoflefthandandax = Vector3.Distance(pickax.transform.position, leftHand.position); 
         distanceofrighthandandax = Vector3.Distance(pickax.transform.position, righthand.transform.position);
    }


    void pickaxintorighthand()
    {
        if (distanceofrighthandandax < mindistancetopickup)
        {
            //Collider colide = righthand.GetComponent<Collider>();
            //colide.enabled = false;
            pickax.transform.SetParent(righthand.transform);
            pickax.transform.localPosition = Vector3.zero;  // Reset local position
            //pickax.transform.localRotation = Quaternion.identity;
            Rigidbody rb = pickax.GetComponent<Rigidbody>();
            rb.useGravity = false;
            
            inhand = true;

        }
    }


    void Dropingthepickax()
    {
        
        pickax.transform.SetParent(null);
        Rigidbody rb = pickax.GetComponent<Rigidbody>();
         rb.useGravity = true;


        //Collider colide = righthand.GetComponent<Collider>();
        //colide.enabled = true;
        inhand = false;
    }

}
