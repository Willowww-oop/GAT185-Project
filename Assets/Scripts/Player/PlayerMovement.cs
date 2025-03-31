using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField] Animator animator;
    private Vector2 moveVector;
    private Vector2 lookVector;
    private Rigidbody rb;
    private CapsuleCollider cc;
    private int jumpCount = 0;
    //public LayerMask ground = ~8;
    public bool onGround;

    [SerializeField] float gravity = -2.5f;
    [SerializeField] float speed = 8;
    //[SerializeField] float slideSpeed = 15;
    [SerializeField] float jumpForce = 100;
    [SerializeField] float sensitivity = 0.7f;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] new Transform camera;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        pInput = new PlayerInput();
        pInput.Player.Move.performed += ctx =>
        {
            moveVector = ctx.ReadValue<Vector2>();
        };

        pInput.Player.Move.canceled += ctx =>
        {
            moveVector = Vector2.zero;
        };

        pInput.Player.Look.performed += ctx =>
        {
            // We're expecting this to be a delta, not the actual look vector
            lookVector += ctx.ReadValue<Vector2>() * sensitivity;

            lookVector.y = Mathf.Clamp(lookVector.y, -90, 90);
        };  
        pInput.Player.Jump.performed += Jump_performed;
        pInput.Player.Attack.performed += Attack_performed;
        //pInput.Player.Crouch.performed += Crouch_performed;

        animator.SetFloat("Speed", speed);
        animator.SetFloat("AirSpeed", jumpForce);
        animator.SetBool("OnGround", onGround);
        
        //pInput.Player.Crouch.canceled += Crouch_canceled;
    }
    void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (jumpCount < 2)
        {
            rb.AddForce(0, Mathf.Sqrt(-2 * jumpForce * gravity), 0);
        }
        animator.SetTrigger("Jump");
        onGround = false;
        jumpCount++;
    }

    void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        animator.SetTrigger("Attack");
    }

    //void Crouch_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{ 
    //    rb.AddForce(cc.direction, 0, cc.direction);
    //}   
    //void Crouch_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{

    //}




    //void Jump_cancelled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    //{

    //}

    void OnEnable()
    {
        pInput.Player.Enable();

    }

    void OnDisable()
    {
        pInput.Player.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (((ground >> collision.gameObject.layer) & 1) != 0)
        //{
        //    jumpCount = 0;
        //}

        onGround = true;

        if (onGround)
        { 
            jumpCount = 0; 
            
        }

        else
        {
            maxSpeed = speed - maxSpeed;

        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 up = Vector3.up;

        // Calculating what direction you're facing
        Quaternion faceDirection = Quaternion.AngleAxis(lookVector.x, up);

        //transform.position += faceDirection * new Vector3(moveVector.x, 0, moveVector.y) * (speed * Time.fixedDeltaTime);
        rb.AddForce( faceDirection * new Vector3(moveVector.x, 0, moveVector.y) * speed);


        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed  );
    }

    void LateUpdate()
    {
        Vector3 up = Vector3.up;
        Vector3 right = Vector3.right;

        // Camera rotation
        camera.rotation = Quaternion.identity;
        camera.Rotate(up, lookVector.x);
        camera.Rotate(right, -lookVector.y);
    }
}