using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private float MaxDistance = 10f;

    private Transform LaserTransform;
    private LineRenderer LaserLineRenderer;

    private Color MainLaserColor;

    void Start()
    {
        LaserTransform = transform.Find("LaserBeam");
        if (LaserTransform == null)
        {
            throw new MissingComponentException("A child object named LaserBeam is required");
        }
        LaserLineRenderer = transform.Find("LaserBeam").GetComponent<LineRenderer>();
        if (LaserLineRenderer == null)
        {
            throw new MissingComponentException("LaserBeam requires a LineRenderer");
        }
        LaserLineRenderer.positionCount = 2;
        LaserLineRenderer.useWorldSpace = false;
        LaserLineRenderer.SetPosition(0, Vector3.zero);

        MainLaserColor = LaserLineRenderer.material.color;
    }

    void Update()
    {
        if (Physics.Raycast(LaserTransform.position, LaserTransform.forward, out RaycastHit raycastHit, MaxDistance))
        {
            LaserLineRenderer.SetPosition(1, Vector3.forward * raycastHit.distance);

            if (raycastHit.collider.GetComponent<CharacterController>() == null)
            {
                OnPlayerNotHit();
            }
            else
            {
                OnPlayerHit();
            }
        }
        else
        {
            LaserLineRenderer.SetPosition(1, Vector3.forward * MaxDistance);
            OnPlayerNotHit();
        }
    }

    private void OnPlayerHit()
    {
        LaserLineRenderer.material.color = Color.white;
    }

    private void OnPlayerNotHit()
    {
        LaserLineRenderer.material.color = MainLaserColor;
    }
}
