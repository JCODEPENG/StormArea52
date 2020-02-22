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

    [Tooltip("Determines which controller controls this player")]
    [SerializeField] private int PlayerNumber = 1;

    [Header("Movement")]
    [SerializeField] private float MovementForce = 100f;
    [SerializeField] private float KnockedDownMovementForce = 10f;

    private GameObject SpriteObject;
    [SerializeField] private GameObject PlayerUIObject;

    private Rigidbody rb;
    private Vector3 CurrentMovementDirection = Vector3.zero;
    private Vector3 col_pos;
    public int score { get; private set; } = 0;
    public GameObject collectable;
    public Text scoretext;
    private Animator animator;
    private Vector3 scale;
    private Vector3 rotate;

    private bool isKnockedDown = false;

    // Start is called before the first frame update
    void Start()
    {
        // get reference to Rigidbody so we can apply forces to it to move the character around
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            throw new MissingComponentException("Character controller needs a rigidbody");
        }
        SpriteObject = GetComponentInChildren<SpriteRenderer>().gameObject;

        PlayerUIObject.SetActive(false);

        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.GAME_OVER_LOSE, NotMove);
        animator = GetComponentInChildren<Animator>();
        rotate = transform.GetChild(0).transform.eulerAngles;
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMovementDirection = GetMovementDirectionFromPlayerInputs();
        transform.GetChild(0).transform.eulerAngles = rotate;
        transform.localScale = scale;
        if (IsThrowButtonPressed && score>0)
        {
            //Instantiate(collectable,new Vector3(col_pos.x,col_pos.y,col_pos.z), Quaternion.identity);
            Instantiate(
                collectable,
                transform.position + (new Vector3(2f, 0.5f, Random.Range(0f, 0.3f))),
                Quaternion.identity
            );
            score--;
            //scoretext.text = "Player score: " + score.ToString();
            Debug.Log(score);
        }

        if (isKnockedDown)
        {
            animator.SetBool("isDown", true);
        }
        else
        {
            rotate.y = 0;
            animator.SetBool("isDown", false);
        }
    }

    private bool IsThrowButtonPressed => Input.GetKeyDown(ThrowKey) || Input.GetKeyDown("joystick " + PlayerNumber + " button 2");

    private Vector3 GetMovementDirectionFromPlayerInputs()
    {
        Vector3 movementDirection = Vector3.zero;

        // read keyboard inputs to determine which direction to move in.

        if (Input.GetKey(MoveUpKey))
        {
            if (animator.GetBool("isDown")){
                rotate.y = 0;
            }
            movementDirection += transform.forward;
            col_pos.Set(rb.position.x, rb.position.y, rb.position.z+2); //for collectable's position
        }
        if (Input.GetKey(MoveLeftKey))
        {
            if (animator.GetBool("isDown")){
                rotate.y = -90;
            }
            scale.x = -Mathf.Abs(scale.x);
            movementDirection -= transform.right;
            col_pos.Set(rb.position.x - 2, rb.position.y , rb.position.z);

        }
        if (Input.GetKey(MoveDownKey))
        {
            if (animator.GetBool("isDown")){
                rotate.y = 180;
            }
            movementDirection -= transform.forward;
            col_pos.Set(rb.position.x, rb.position.y , rb.position.z-2);

        }
        if (Input.GetKey(MoveRightKey))
        {
            if (animator.GetBool("isDown")){
                rotate.y = 90;
            }
            scale.x = Mathf.Abs(scale.x);
            movementDirection += transform.right;
            col_pos.Set(rb.position.x + 2, rb.position.y , rb.position.z);

        }
        movementDirection += Input.GetAxis("P" + PlayerNumber.ToString() + "Horizontal") * transform.right;
        movementDirection += Input.GetAxis("P" + PlayerNumber.ToString() + "Vertical") * transform.forward;


        return movementDirection;
    }

    private float CurrentMovementForce => isKnockedDown ? KnockedDownMovementForce : MovementForce;

    private void FixedUpdate()
    {
        animator.SetBool("isRunning", false);
        if (CurrentMovementDirection != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
            if (CurrentMovementDirection.magnitude > 1f)
            {
                CurrentMovementDirection = CurrentMovementDirection.normalized;
            }

            CurrentMovementDirection *= CurrentMovementForce;
        }
        rb.velocity = new Vector3(CurrentMovementDirection.x, rb.velocity.y, CurrentMovementDirection.z);
    }

    private void NotMove(){
        MovementForce = 0f;
        KnockedDownMovementForce = 0f;
    }

    public float CurrentMovementSpeed => rb.velocity.magnitude;

    public bool IsActionKeyPressed()
    {
        if (Input.GetKeyDown(ActionKey))
        {
            return true;
        }
        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 1"))
        {
            return true;
        }
        return false;
    }

     void OnCollisionEnter (Collision coll)
    {
        if (coll.gameObject.CompareTag("col")) 
        {
            score++;
           // scoretext.text = "Player score: " + score.ToString();
            Destroy(coll.gameObject);
            Debug.Log(score);

        }
        if (coll.gameObject.CompareTag("Player"))
        {
            // revive other player is able to
            coll.gameObject.GetComponent<CharacterController>().Revive();
        }
    }

    public void KnockDown()
    {
        isKnockedDown = true;
        PlayerUIObject.SetActive(true);
    }

    public void Revive()
    {
        isKnockedDown = false;
        PlayerUIObject.SetActive(false);
    }
}
