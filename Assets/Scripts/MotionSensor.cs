using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of a motion sensor with a laser beam and status light
/// </summary>
public class MotionSensor : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private float MaxDistance = 10f;
    [SerializeField] private float MovementSpeedDetectionThreshold = 0.1f;

    public float GetMaxDistance => MaxDistance;

    private Transform LaserTransform;
    private LineRenderer LaserLineRenderer;
    private MeshRenderer StatusLightRenderer;

    private Color MainLaserColor;

    void Start()
    {
        // get references to needed components
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

        StatusLightRenderer = transform.Find("DeviceModel/StatusLight").GetComponent<MeshRenderer>();
        if (StatusLightRenderer == null)
        {
            throw new MissingComponentException("Status light needs a mesh renderer");
        }


        // initialize line renderer
        LaserLineRenderer.positionCount = 2;
        LaserLineRenderer.useWorldSpace = false;
        LaserLineRenderer.SetPosition(0, Vector3.zero);
        MainLaserColor = LaserLineRenderer.material.color;
    }

    void Update()
    {
        // use a raycast to see what object the laser hits
        if (Physics.Raycast(LaserTransform.position, LaserTransform.forward, out RaycastHit raycastHit, MaxDistance))
        {
            // raycast hit something, so only show the laser up to that point
            LaserLineRenderer.SetPosition(1, Vector3.forward * raycastHit.distance);

            // if the raycast hit a player, and the laser is detecting movement, then register the player as seen
            CharacterController hitCharacter = raycastHit.collider.GetComponent<CharacterController>();
            if (hitCharacter != null && hitCharacter.CurrentMovementSpeed > MovementSpeedDetectionThreshold)
            {
                OnPlayerHit(hitCharacter);
            }
            else
            {
                OnPlayerNotHit();
            }
        }
        else
        {
            // laser hit nothing, so just show the laser at full distance
            LaserLineRenderer.SetPosition(1, Vector3.forward * MaxDistance);
            OnPlayerNotHit();
        }
    }

    private void OnPlayerHit(CharacterController character)
    {
        LaserLineRenderer.material.color = Color.white;
        StatusLightRenderer.material.color = Color.red;

        character.KnockDown();
    }

    private void OnPlayerNotHit()
    {
        LaserLineRenderer.material.color = MainLaserColor;
        StatusLightRenderer.material.color = Color.white;
    }
}
