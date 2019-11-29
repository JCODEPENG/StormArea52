using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressuerePlate_FField : MonoBehaviour
{
    public GameObject field; //any object which will be activated/deactivated
    int ch = 0; //first check condition(for material)
    int obj = 0; //amount of objects on a plate
    private Color old;
    void OnTriggerEnter(Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        if (ch == 0)
        {
            old = render.material.color;
            ch = 1;
        }
        field.active = false;
        render.material.color = Color.green;
        obj++;
    }

    void OnTriggerExit(Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        obj--;
        if (obj == 0) { 
        render.material.color = old;
        field.active = true;
    }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
