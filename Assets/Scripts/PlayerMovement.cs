using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;        // Movement speed
    [SerializeField] private float rotationSpeed = 200f; // Rotation speed
    [SerializeField] private float jumpForce = 2f;

    private float normalSpd;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float timer;
    private Animator anim;
    private bool isGrounded; public bool hasLanded => isGrounded;
    public static UnityEvent endGame = new();

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        normalSpd = moveSpeed;
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Jump();
        Run();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float move = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Calculate movement direction
        moveDirection = transform.forward*move;

        // Apply velocity to the Rigidbody (for smooth physics movement)
        Vector3 velocity = moveDirection * moveSpeed;
        if (move != 0)
        {
            anim.SetBool("Walking", true);
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }
        else anim.SetBool("Walking", false);
    }

    void HandleRotation()
    {
        float rotInput = Input.GetAxis("Horizontal"); // A/D or Left/Right

        // Calculate rotation based on input
        float rot = rotInput * rotationSpeed * Time.fixedDeltaTime;

        // Apply rotation to the Rigidbody
        Quaternion rotate = Quaternion.Euler(0f, rot, 0f);
        rb.MoveRotation(rb.rotation * rotate);
    }
    private bool CheckGround()
    {
        // Perform an OverlapBox check with added offset
        Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0, 0.2f, 0), transform.localScale / 2, Quaternion.identity, LayerMask.GetMask("Ground"));
        return isGrounded = colliders.Length > 0 ? true: false;
    }
    void Jump()
    {
        //Player can only jump once every sec
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var jumpforce = Input.GetKey(KeyCode.LeftControl) ? jumpForce / 2 : jumpForce;
            

            if (CheckGround() && timer > 1)
            {
                rb.AddForce(Vector3.up * jumpforce);
                timer = 0;
            }
        }
    }
    void Run()
    {
            moveSpeed = Input.GetKey(KeyCode.LeftShift) ? normalSpd * 2 : normalSpd;
    }
    public void ChangeJumpForce(float newVal)
    {
        jumpForce = newVal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            endGame.Invoke();
        }
    }

    public float GetStatsJumpForce()
    {
        return jumpForce;
    }
    private void OnDrawGizmos()
    {
        // Set Gizmos color
        Gizmos.color = Color.green;

        // Draw the box (solid or wireframe)
        Gizmos.DrawCube(transform.position + new Vector3(0, 0.2f, 0), transform.localScale/2);
    }

}