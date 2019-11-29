using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_FField : MonoBehaviour
{
    public GameObject field; //any object which will be activated/deactivated
    void OnTriggerStay(Collider player)
    {
        CharacterController playerController = player.GetComponent<CharacterController>();
        if (player.tag == "Player"){
            if (Input.GetKeyDown(playerController.ActionKey))
            {
                Renderer render = GetComponent<Renderer>();
                if (field.gameObject.active == true)
                {
                    field.gameObject.active = false;
                    render.material.color = Color.green;
                }
                else { 
                    field.gameObject.active = true;
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
