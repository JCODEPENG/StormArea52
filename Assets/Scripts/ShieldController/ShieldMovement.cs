using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{

    [Header("Controls")]
    [SerializeField] private KeyCode MoveUpKey = KeyCode.W;
    [SerializeField] private KeyCode MoveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode MoveDownKey = KeyCode.S;
    [SerializeField] private KeyCode MoveRightKey = KeyCode.D;
    [SerializeField] private KeyCode ThrowKey = KeyCode.Space;
    [SerializeField] public KeyCode ActionKey = KeyCode.E;
    private Vector3 CurrentMovementDirection = Vector3.zero;
    [SerializeField] private float MovementForce = 10f;

    private Vector3 col_pos;
    private Rigidbody rb;
    private Vector3 GetMovementDirectionFromPlayerInputs()
    {
        Vector3 movementDirection = Vector3.zero;

        // read keyboard inputs to determine which direction to move in.

        if (Input.GetKey(MoveUpKey))
        {
            movementDirection += transform.forward;
            col_pos.Set(rb.position.x, rb.position.y, rb.position.z + 2); //for collectable's position
        }
        if (Input.GetKey(MoveLeftKey))
        {
            movementDirection -= transform.right;
            col_pos.Set(rb.position.x - 2, rb.position.y, rb.position.z);

        }
        if (Input.GetKey(MoveDownKey))
        {
            movementDirection -= transform.forward;
            col_pos.Set(rb.position.x, rb.position.y, rb.position.z - 2);

        }
        if (Input.GetKey(MoveRightKey))
        {
            movementDirection += transform.right;
            col_pos.Set(rb.position.x + 2, rb.position.y, rb.position.z);

        }
        //movementDirection += Input.GetAxis("P" + PlayerNumber.ToString() + "Horizontal") * transform.right;
        //movementDirection += Input.GetAxis("P" + PlayerNumber.ToString() + "Vertical") * transform.forward;


        return movementDirection;
    }

    // Start is called before the first frame update
    void Start()
    {
        // get reference to Rigidbody so we can apply forces to it to move the character around
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            throw new MissingComponentException("Character controller needs a rigidbody");
        }

    }


    // Update is called once per frame
    void Update()
    {
        CurrentMovementDirection = GetMovementDirectionFromPlayerInputs();
    }

    public float CurrentMovementSpeed => rb.velocity.magnitude;

    private void FixedUpdate()
    {
        if (CurrentMovementDirection != Vector3.zero)
        {
            if (CurrentMovementDirection.magnitude > 1f)
            {
                CurrentMovementDirection = CurrentMovementDirection.normalized;
            }
            CurrentMovementDirection *= MovementForce;
        }
        rb.velocity = new Vector3(CurrentMovementDirection.x, rb.velocity.y, CurrentMovementDirection.z);
    }



}


