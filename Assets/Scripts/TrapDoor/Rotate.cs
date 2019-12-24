using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float timetomove = 1f;



    public void ActivateTrapdoor()
    {
        StartCoroutine(RotateaBit());
    }

    private IEnumerator RotateaBit()
    {
        Vector3 oldpos = transform.eulerAngles;
        Vector3 newpos = oldpos + (Vector3.right * 90);

        float start_time = 0f;
        while (start_time < timetomove)
        {
            transform.eulerAngles = Vector3.Lerp(oldpos, newpos, start_time / timetomove);
            start_time += Time.deltaTime;
            yield return null;
        }
    }

    public void DeactivateTrapdoor()
    {
        StartCoroutine(UnRotateABit());
    }

    private IEnumerator UnRotateABit()
    {

        Vector3 oldpos = transform.eulerAngles;
        Vector3 newpos = oldpos + (Vector3.right * -90);

        float start_time = 0f;
        while (start_time < timetomove)
        {
            transform.eulerAngles = Vector3.Lerp(oldpos, newpos, start_time / timetomove);
            start_time += Time.deltaTime;
            yield return null;
        }
    }

}
