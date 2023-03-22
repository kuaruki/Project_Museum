using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultipier;
    bool readyToJump;


    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;



    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        startYScale = transform.localScale.y; //saves the inicial y of the player in the startYScale variable
    }

    // Update is called once per frame
    private void Update()
    {
        //Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        myInput();
        speedControl();
        stateHandler();

        //Handle Drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }
    private void FixedUpdate()
    {
        movePlayer();
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jumping
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
        
        //start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            //this shrinks the player so further crouching should be done differently, maybe, unless that doesn't affect the future blender asset
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            //This line makes sure that the player doesn't float
        }
        //stop crouch
        if (Input.GetKeyUp(crouchKey))
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        
    }

    private void stateHandler()
    {
        //Mode - crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        //Mode - Sprinting 
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;   
        }
        
        //Mode - walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }

    }


    private void movePlayer()
    {
        //Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; //walking in  the direction the player is looking

        //on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //on air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultipier, ForceMode.Force);

    }


    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed) //if you go faster than the movement speed
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; //calculate what the max velocity would be
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); // and apply it
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //.Impulse because it'll only apply the force once
    }
    private void resetJump()
    {
        readyToJump = true;
    }

}