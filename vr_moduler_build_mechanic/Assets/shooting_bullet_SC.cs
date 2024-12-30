using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_bullet_SC : MonoBehaviour
{
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private Transform muzzel;
    [SerializeField] private float bulletspeed;

    public void shootbullet()
    {
        GameObject bulletspawn = Instantiate(bullet_Prefab,muzzel.position, Quaternion.identity);
        Rigidbody bullet_Prefabrb = GetComponent<Rigidbody>();
        bullet_Prefabrb.velocity = transform.forward * bulletspeed;
    }
}
