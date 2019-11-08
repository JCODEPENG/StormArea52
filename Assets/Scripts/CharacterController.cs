using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private KeyCode Throw = KeyCode.Space;

    [Header("Movement")]
    [SerializeField] private float MovementForce = 100f;
    public GameObject Capsule;
    public Text countText;
    private Rigidbody rb;
    private Vector3 CurrentMovementDirection = Vector3.zero;
    private int count;
    
    // Start is called before the first frame update
    void Start()
    {
        // get reference to Rigidbody so we can apply forces to it to move the character around
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            throw new MissingComponentException("Character controller needs a rigidbody");
        }
        count = 0;
        SetCountText();

       
    }



    // Update is called once per frame
    void Update()
    {
        CurrentMovementDirection = GetMovementDirectionFromPlayerInputs();

        if (count > 0 & Input.GetKey(Throw))
        {
            count--;
            SetCountText();
            Instantiate(Capsule, new Vector3(rb.position.x, rb.position.y, rb.position.z+1), Quaternion.identity);
            
            //Instantiate(Capsule, rb.position.x), Quaternion.identity);
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Capsule"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }
}
