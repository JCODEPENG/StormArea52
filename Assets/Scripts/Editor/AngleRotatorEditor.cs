using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom Editor script to show the angle range of an AngleRotator
/// </summary>
[CustomEditor(typeof(AngleRotator))]
[CanEditMultipleObjects]
public class AngleRotatorEditor : Editor
{
    void OnSceneGUI()
    {
        AngleRotator angleRotator = (AngleRotator)target;
        Transform transform = angleRotator.transform;

        float rangeSize = 1f;

        // if this rotator is attached to a motion sensor, show the range of the motion sensor instead of the default 1.0
        MotionSensor motionSensor = angleRotator.GetComponent<MotionSensor>();
        if (motionSensor != null)
        {
            rangeSize = motionSensor.GetMaxDistance;
        }

        // use arcs and lines to draw the angle range
        Handles.color = angleRotator.GetReverseInitialDirection ? Color.black : Color.white;
        DrawArcRange(transform, angleRotator.GetAngleRange, rangeSize);
        Handles.color = angleRotator.GetReverseInitialDirection ? Color.white : Color.black;
        DrawArcRange(transform, -angleRotator.GetAngleRange, rangeSize);

        // draw starting angle
        Handles.color = Color.blue;
        Handles.DrawLine(
            transform.position,
            transform.position + Quaternion.AngleAxis(angleRotator.GetInitialAngle, transform.up) * transform.forward * rangeSize
        );
    }

    private void DrawArcRange(Transform transform, float angle, float distance)
    {
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, angle, distance);

        Handles.DrawLine(
            transform.position,
            transform.position + Quaternion.AngleAxis(angle, transform.up) * transform.forward * distance
        );
    }
}
