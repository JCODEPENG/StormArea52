using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls a playable character using a Rigidbody
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private KeyCode MoveUpKey = KeyCode.W;
    [SerializeField] private KeyCode MoveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode MoveDownKey = KeyCode.S;
    [SerializeField] private KeyCode MoveRightKey = KeyCode.D;

    [Header("Movement")]
    [SerializeField] private float MovementForce = 100f;

    private Rigidbody rb;
    private Vector3 CurrentMovementDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // get reference to Rigidbody so we can apply forces to it to move the character around
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            throw new MissingComponentException("Character controller needs a rigidbody");
        }
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.GAME_OVER_LOSE, NotMove);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMovementDirection = GetMovementDirectionFromPlayerInputs();
    }

    private Vector3 GetMovementDirectionFromPlayerInputs()
    {
        Vector3 movementDirection = Vector3.zero;

        // read keyboard inputs to determine which direction to move in.
        if (Input.GetKey(MoveUpKey))
        {
            movementDirection += transform.forward;
        }
        if (Input.GetKey(MoveLeftKey))
        {
            movementDirection -= transform.right;
        }
        if (Input.GetKey(MoveDownKey))
        {
            movementDirection -= transform.forward;
        }
        if (Input.GetKey(MoveRightKey))
        {
            movementDirection += transform.right;
        }

        return movementDirection;
    }

    // Physics updates on FixedUpdate, so apply movement forces here instead of in Update()
    private void FixedUpdate()
    {
        if (CurrentMovementDirection != Vector3.zero)
        {
            rb.AddForce(CurrentMovementDirection.normalized * MovementForce);
        }
    }
    private void NotMove(){
        MovementForce = 0f;
    }

    public float CurrentMovementSpeed => rb.velocity.magnitude;
}
