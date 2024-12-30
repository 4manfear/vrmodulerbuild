using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_collection : MonoBehaviour
{
    public CollectibleManager collectibleManager;

    public enum ItemType { Rock, Coal }
    public ItemType itemType;  // The type of the item (Rock or Coal)

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player's hand
        if (collision.gameObject.CompareTag("right_hand") || collision.gameObject.CompareTag("left_hand"))
        {
            if(itemType== ItemType.Rock)
            {
                collectibleManager.CollectRock();
            }
            if(itemType== ItemType.Coal)
            {
                collectibleManager.CollectCoal();
            }
        }
    }
    private void Update()
    {
        collectibleManager = GameObject.FindObjectOfType<CollectibleManager>();
    }
    private void CollectItem()
    {
       
    }
}
