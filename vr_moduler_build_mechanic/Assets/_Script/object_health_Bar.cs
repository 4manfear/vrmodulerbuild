using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_health_Bar : MonoBehaviour
{

    public testvrcuntroller vc;

    public float orignal_Health;

    public float currenthealth;

    public float damagetaken;


    public GameObject itemToDrop; // The item to drop when the object is destroyed
    public Transform dropPoint; // The point where the item will be dropped
    public int dropAmount = 1; // The number of items to drop


    private void Awake()
    {
        currenthealth = orignal_Health;
    }

    private void Update()
    {
        if (currenthealth <= 0)
        {
            DropItems();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (vc.inhand == true)
        {
            if(collision.gameObject.CompareTag("pickax"))
            {
                currenthealth -= damagetaken;
                Debug.Log(currenthealth);
            }
        }
    }

    void DropItems()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            // Instantiate the dropped item at the drop point
            Instantiate(itemToDrop, dropPoint.position, Quaternion.identity);
        }
    }


}
