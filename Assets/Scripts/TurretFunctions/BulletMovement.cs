﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Transform pos;
    Vector3 force;


    private Transform RespawnPoint;

    //float step = 1.0f;

    float thrust = 5000.0f;
    private void Update()
    {

        if (this.CompareTag("NormalBullet"))
        {
            //moves bullet forward
            transform.position += transform.forward * Time.deltaTime * 30f;
            force = transform.forward;
        }
        if (this.CompareTag("KillBullet"))
        {
            //moves bullet forward
            transform.position = Vector3.MoveTowards(transform.position, pos.position, 20f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(0, 0, 0);
        
        //if bullet hits the player it will cause the player to thrust forward
        if (other.CompareTag("Player"))
        {
            Rigidbody playercomponent = other.GetComponent<Rigidbody>();
            if (playercomponent == null)
            {
                Debug.Log("no rigid body");
                return;
            }
            else
            {
                gameObject.SetActive(false);
                if (this.CompareTag("NormalBullet"))
                {
                    playercomponent.AddForce(force * thrust);
                    gameObject.SetActive(false);
                }
                if (this.CompareTag("KillBullet"))
                {
                    other.transform.position = RespawnPoint.transform.position;
                    gameObject.SetActive(false);
                }
                gameObject.SetActive(false);
            }

        }
        gameObject.SetActive(false);



    }

    public void SetTracker(GameObject go)
    {
        pos = go.transform;
    }


    public void SetRespawnPoint(Transform RespawnPoint)
    {
        this.RespawnPoint = RespawnPoint;
    }
}
