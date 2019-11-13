using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float delay = .9f;
    [SerializeField] float followDistance = 10f;

    private GameObject[] followingSubjectList;
    private Vector3 offsetBetweenTarget;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        followingSubjectList = GameObject.FindGameObjectsWithTag("Player");
        if (followingSubjectList.Length < 1)
        {
            Debug.LogWarning("Camera controller has no subjects to follow", this);
        }
        Vector3 pointingDirection = ((this.transform.rotation * Vector3.forward));

        offsetBetweenTarget = -pointingDirection * followDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 targetPosition = CalculateCameraPosition(GetAvgSubjectsPosition());

        // Interpolate the position of camera/ move to the new position
        velocity = ((targetPosition - this.transform.position) / delay) * Time.deltaTime;
        this.transform.position += velocity;
    }

    public void SetFollowDistance(float distance)
    {
        followDistance = distance;

        Vector3 pointingDirection = ((this.transform.rotation * Vector3.forward));
        offsetBetweenTarget = -pointingDirection * followDistance; 
    }

    private Vector3 GetAvgSubjectsPosition()
    {
        Vector3 avgPosition = Vector3.zero;

        foreach (GameObject subject in followingSubjectList)
        {
            avgPosition += subject.transform.position;
        }

        avgPosition /= followingSubjectList.Length;
        return avgPosition;
    }

    private Vector3 CalculateCameraPosition(Vector3 targetPosition)
    {
        return targetPosition + offsetBetweenTarget;
    }
}
