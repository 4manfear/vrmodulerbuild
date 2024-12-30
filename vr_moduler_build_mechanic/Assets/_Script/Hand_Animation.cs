using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Animation : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggeraction;
    [SerializeField] private InputActionProperty gripAction;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        float triggerValue = triggeraction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        anim.SetFloat("Trigger", triggerValue);
        anim.SetFloat("Grip", gripValue);

    }
}
