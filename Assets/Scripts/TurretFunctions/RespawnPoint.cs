using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    //checks if the player collides with death zone 
    [SerializeField] private Transform respawnPoint = default;

    GameObject invisible;

    void OnTriggerEnter(Collider other)
    { 
        other.transform.position = respawnPoint.transform.position;
    }
}
