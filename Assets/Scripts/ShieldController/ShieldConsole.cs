using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldConsole : MonoBehaviour
{
    public GameObject field; //any object which will be activated/deactivated
    bool PlayerIsControlled = true;
    void OnTriggerStay(Collider player)
    {
        CharacterController playerController = player.GetComponent<CharacterController>();
        if (player.tag == "Player")
        {
            if (playerController.IsActionKeyPressed())
            {
                Renderer render = GetComponent<Renderer>();
                if (PlayerIsControlled == true)
                {
                    PlayerIsControlled = false;
                    render.material.color = Color.green;


                }
                else
                {
                    PlayerIsControlled = true;
                    render.material.color = Color.red;

                }
            }

        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
