using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootBase : MonoBehaviour
{

    public Transform muzzle;
    public GameObject bullet;



    public virtual void Shoot(GameObject go)
    {
        //instantiates a bullet
        GameObject missilego = Instantiate(bullet, muzzle.transform.position, muzzle.rotation);
        missilego.GetComponent<BulletMovement>().SetTracker(go);

        //missilego.transform.position += transform.forward * Time.deltaTime;
       
    }
    

}
