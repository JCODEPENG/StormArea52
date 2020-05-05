using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{

    [Header("Controls")]
    [SerializeField] private KeyCode MoveUpKey;
    [SerializeField] private KeyCode MoveLeftKey;
    [SerializeField] private KeyCode MoveDownKey;
    [SerializeField] private KeyCode MoveRightKey;
    private Vector3 CurrentMovementDirection = Vector3.zero;
    [SerializeField] private float MovementForce = 10f;

    private Vector3 col_pos;
    private Rigidbody rb;
    private Vector3 DoorOrientationMovement(Vector3 movementDirection, bool turned) {
        if (!turned)
        {
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
            return movementDirection;
        }
        else
        {
            Debug.Log("heelow world");
            if (Input.GetKey(MoveUpKey))
            {
                movementDirection -= transform.right;
                col_pos.Set(rb.position.x, rb.position.y, rb.position.z + 2); //for collectable's position
            }
            if (Input.GetKey(MoveLeftKey))
            {
                movementDirection -= transform.forward;
                col_pos.Set(rb.position.x - 2, rb.position.y, rb.position.z);

            }
            if (Input.GetKey(MoveDownKey))
            {
                movementDirection += transform.right;
                col_pos.Set(rb.position.x, rb.position.y, rb.position.z - 2);

            }
            if (Input.GetKey(MoveRightKey))
            {
                movementDirection += transform.forward;
                col_pos.Set(rb.position.x + 2, rb.position.y, rb.position.z);

            }
            return movementDirection;
        }
    }

    private Vector3 GetMovementDirectionFromPlayerInputs()
    {
        Vector3 movementDirection = Vector3.zero;

        if (transform.rotation.eulerAngles.y == 0)
        {
            movementDirection = DoorOrientationMovement(movementDirection, false);  
        }
        if (transform.rotation.eulerAngles.y == 90)
        {
            movementDirection = DoorOrientationMovement(movementDirection, true);  
        }
        return movementDirection;
    }

    private void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("col"))
        {
            Physics.IgnoreCollision(other.collider, rb.GetComponent<Collider>());
        }
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

    public void SetUpKey(KeyCode code)
    {
        MoveUpKey = code;
    }
    public void SetDownKey(KeyCode code)
    {
        MoveDownKey = code;
    }
    public void SetLeftKey(KeyCode code)
    {
        MoveLeftKey = code;
    }
    public void SetRightKey(KeyCode code)
    {
        MoveRightKey = code;
    }



}


