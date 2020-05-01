using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldConsole : MonoBehaviour
{
    public GameObject field; //any object which will be activated/deactivated
    public bool PlayerIsControlled = true;
    
    //public ShieldMovement Controller;
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
                    field.GetComponent<ShieldMovement>().enabled = true;
                    PlayerIsControlled = false;
                    render.material.color = Color.green;


                }
                else
                {
                    field.GetComponent<ShieldMovement>().enabled = false;
                    PlayerIsControlled = true;
                    render.material.color = Color.red;

                }
            }

        }
    }

    void Start()
    {
        field.GetComponent<ShieldMovement>().enabled = false;

    }

    void Update()
    {

    }
}
