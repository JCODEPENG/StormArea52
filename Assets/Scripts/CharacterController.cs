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
    [SerializeField] private KeyCode ThrowKey = KeyCode.Space;
    [SerializeField] public KeyCode ActionKey = KeyCode.E;

    [Header("Movement")]
    [SerializeField] private float MovementForce = 100f;

    private Rigidbody rb;
    private Vector3 CurrentMovementDirection = Vector3.zero;
    private Vector3 col_pos;
    int score = 0;
    public GameObject collectable;
    public Text scoretext;
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
        if (Input.GetKeyDown(ThrowKey) && score>0)
        {
            Instantiate(collectable,new Vector3(col_pos.x,col_pos.y,col_pos.z), Quaternion.identity);
            score--;
            scoretext.text = "Player score: " + score.ToString();
            Debug.Log(score);
        }
    }

    private Vector3 GetMovementDirectionFromPlayerInputs()
    {
        Vector3 movementDirection = Vector3.zero;

        // read keyboard inputs to determine which direction to move in.

        if (Input.GetKey(MoveUpKey))
        {
            movementDirection += transform.forward;
            col_pos.Set(rb.position.x, rb.position.y, rb.position.z+2); //for collectable's position
        }
        if (Input.GetKey(MoveLeftKey))
        {
            movementDirection -= transform.right;
            col_pos.Set(rb.position.x - 2, rb.position.y , rb.position.z);

        }
        if (Input.GetKey(MoveDownKey))
        {
            movementDirection -= transform.forward;
            col_pos.Set(rb.position.x, rb.position.y , rb.position.z-2);

        }
        if (Input.GetKey(MoveRightKey))
        {
            movementDirection += transform.right;
            col_pos.Set(rb.position.x + 2, rb.position.y , rb.position.z);

        }

        return movementDirection;


    }

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

     void OnCollisionEnter (Collision coll)
    {
        if (coll.gameObject.CompareTag("col")) 
        {
            score++;
            scoretext.text = "Player score: " + score.ToString();
            Destroy(coll.gameObject);
            Debug.Log(score);

        }
    }
   
    
    }
