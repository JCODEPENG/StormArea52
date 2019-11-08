using UnityEngine;

/// <summary>
/// Rotates an object using a sine function
/// </summary>
public class AngleRotator : MonoBehaviour
{
    [Header("Rotation function")]
    [SerializeField] private float AngleRange = 45f;
    [SerializeField] private float CycleTime = 1f;

    [Header("Initial values")]
    [SerializeField] private float InitialAngle = 0f;
    [SerializeField] private bool ReverseInitialDirection = false;

    // public getters for the editor script
    public float GetAngleRange => AngleRange;
    public float GetInitialAngle => InitialAngle;
    public bool GetReverseInitialDirection => ReverseInitialDirection;

    private Vector3 BaseRotation;
    private float timeAccumulator;

    void Start()
    {
        BaseRotation = transform.localEulerAngles;

        if (AngleRange < 0f)
        {
            throw new System.InvalidOperationException("Angle range must be positive");
        }
        if (CycleTime < 0f)
        {
            throw new System.InvalidOperationException("Cycle time must be positive");
        }
        if (InitialAngle > AngleRange || InitialAngle < -AngleRange)
        {
            throw new System.InvalidOperationException("Initial angle must be within the angle range");
        }

        // set the time accumulator to be so that the initial angle is InitialAngle
        // this looks like a complex calculation, but it's this equation from Update(), but rearranged to solve for timeAccumulator
        // Mathf.Sin((timeAccumulator * Mathf.PI * 2f) / CycleTime) * AngleRange) = InitialAngle
        float initialAngleTime = (CycleTime * Mathf.Asin(InitialAngle / AngleRange)) / (Mathf.PI * 2f);

        // set the time accumulator so that the angle starts at the initial angle, and starts moving in the right direction
        if (ReverseInitialDirection)
        {
            timeAccumulator = (CycleTime / 2f) - initialAngleTime;
        }
        else
        {
            timeAccumulator = initialAngleTime;
        }
    }

    void Update()
    {
        transform.localEulerAngles = BaseRotation + (Vector3.up * Mathf.Sin((timeAccumulator * Mathf.PI * 2f) / CycleTime) * AngleRange);
        timeAccumulator += Time.deltaTime;
    }
}
