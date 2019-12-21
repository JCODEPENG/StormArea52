using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrap : MonoBehaviour
{
    float turnspeed = 40f;
    //public GameObject field; //any object which will be activated/deactivated
    int ch = 0; //first check condition(for material)
    int obj = 0; //amount of objects on a plate
    private Color old;
    public Rotate Rotatefunction;
    void OnTriggerEnter(Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        if (ch == 0)
        {
            old = render.material.color;
            ch = 1;
        }
       Rotatefunction.ActivateTrapdoor();
      // field.transform.Rotate(0, 0, 90);
        render.material.color = Color.green;
        obj++;
    }

    void OnTriggerExit(Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        obj--;
        if (obj == 0)
        {
            render.material.color = old;
            //field.transform.Rotate(0, 0, 90);
        }
    }
    private void Start()
    {

    }
    private void Update()
    {

    }
}
