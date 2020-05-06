using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldConsole : MonoBehaviour
{
    public GameObject field; //any object which will be activated/deactivated
    public bool PlayerIsControlled = true;
    
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
                    field.GetComponent<ShieldMovement>().SetUpKey(playerController.getUpKeyCode());
                    field.GetComponent<ShieldMovement>().SetDownKey(playerController.getDownKeyCode());
                    field.GetComponent<ShieldMovement>().SetLeftKey(playerController.getLeftKeyCode());
                    field.GetComponent<ShieldMovement>().SetRightKey(playerController.getRightKeyCode());
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
