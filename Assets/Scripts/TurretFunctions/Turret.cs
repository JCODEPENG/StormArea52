using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject currenttarget;
    public Transform turrethead;

    public float attackdamage;
    public float shotCoolDown;
    private float timer;
    public float lookspeed = 1f; 
    public float attackdist;

    public bool showRange = false;
    public TurretShootBase shotscript;

    
    // Start is called before the first frame update
    void Start()
    {
        //Constantly checks for the closest target
        InvokeRepeating("CheckForTarget", 0, 0.5f);
        shotscript = GetComponent<TurretShootBase>();
    }

    // Update is called once per frame
    void Update()
    {
        //if there is target then have turret follow it 
        if (currenttarget!= null)
        {
            FollowTarget();
        }

        timer += Time.deltaTime;
        if (timer >= shotCoolDown)
        {
            if(currenttarget != null)
            {
                timer = 0;
                shoot();
            }
        }

    }
    private void CheckForTarget()
    {
        //checks for the player tag and if the distance is less than the max range of the turret
        //it will set player as the target
        Collider[] colls = Physics.OverlapSphere(transform.position, attackdist);
        float distAway = Mathf.Infinity;
        for (int i = 0; i< colls.Length; i++)
        {
            if (colls[i].tag == "Player")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if(dist < distAway)
                {
                    currenttarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget()
    {
        Vector3 targetDir = currenttarget.transform.position - transform.position;
        targetDir.y = 0;
        turrethead.forward = targetDir;
    }
    private void shoot()
    {
        shotscript.Shoot(currenttarget);
    }

    private void OnDrawGizmos()
    {
        if (showRange == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackdist);
        }
    }

}
