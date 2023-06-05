using System;
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
    public LayerMask Targetable;
    bool grounded;
    bool grounded2;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Reference to Inventory script
    public Inventory inventory;
         
    // Reference to HUD script
    public HUD Hud;
    
    // Players hand, where he can see object
    public GameObject Hand;

    public bool mLockPickup;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }



    [SerializeField] private GameObject Pause_Canvas;
    [SerializeField] private Camera MainCamera;

    private void Awake() {
        Time.timeScale = 1; //isto faz com que o jogo comece assim que a cena e carregada,
                            //o que impede o jogo de ficar parado no inicio do nivel assim
                            //que se muda de mapa
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Pause_Canvas.SetActive(false);
        ////locking the cursor and making it not visible
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        readyToJump = true;
        startYScale = transform.localScale.y; //saves the inicial y of the player in the startYScale variable

        // Event handlers
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    // Update is called once per frame
    private void Update()
    {
        //Pick up key INVENTORY
        // If the user presses the e key and there is an item he can pick up
        if (mItemToPickup != null && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(mItemToPickup);
            mItemToPickup.OnPickup();

            Hud.CloseMessagePanel();
        }

        //Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        grounded2 = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Targetable);
        Debug.DrawRay(transform.position, Vector3.down * 10f, Color.red);
        
        myInput();
        speedControl();
        stateHandler();

        //Handle Drag
        if (grounded || grounded2) //drag on the floor
            rb.drag = groundDrag;
        else //drag on air
            rb.drag = 0;

        //Pause mechanic
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause();
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }



    //---------------
    //Player Movement
    //---------------
    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jumping
        if ((Input.GetKey(jumpKey) && readyToJump && grounded) || (Input.GetKey(jumpKey) && readyToJump && grounded2))
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
        else if((grounded && Input.GetKey(sprintKey)) || (grounded2 && Input.GetKey(sprintKey)))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;   
        }
        
        //Mode - walking
        else if (grounded || grounded2)
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
        if (grounded || grounded2)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //on air
        else if (!grounded || !grounded2)
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


    //---------
    //INVENTORY
    //---------
    private IInventoryItem mItemToPickup = null;

    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();

        if (item != null)
        {
            if (mLockPickup)
                return;

            mItemToPickup = item;

            //inventory.AddItem(item);
            //item.OnPickup();

            Hud.OpenMessagePanel("");
        }



        //---------------------------
        //CLOSING DOORS AUTOMATICALLY
        //---------------------------
        if (other.gameObject.CompareTag("ReceptionDoorCheck")) {
            if (MainCamera.GetComponent<Interactor>().isReceptionDoorOpen) {
                Debug.Log("Reception Door Closed");
                //close Reception Door
                MainCamera.GetComponent<Interactor>().ReceptionDoor.transform.rotation = MainCamera.GetComponent<Interactor>().ReceptionDoorInitialRotation;
                MainCamera.GetComponent<Interactor>().isReceptionDoorOpen = false;
            }
        }
        if (other.gameObject.CompareTag("Room1Check")) {
            if (MainCamera.GetComponent<Interactor>().isHallDoorOpen) {
                Debug.Log("Hall Door Closed");
                //close Hall Door
                MainCamera.GetComponent<Interactor>().HallDoor.transform.rotation = MainCamera.GetComponent<Interactor>().HallDoorInitialRotation;
                MainCamera.GetComponent<Interactor>().isHallDoorOpen = false;
            }
        }
        if (other.gameObject.CompareTag("LivrosDoorCheck")) {
            Debug.Log("Livros Door Checked");
            if (MainCamera.GetComponent<Interactor>().isLivrosDoorOpen) {
                Debug.Log("Livros Door Closed");
                //close Livros Door
                MainCamera.GetComponent<Interactor>().LivrosDoor.GetComponent<Room1_Progress>().Jigsaw = false; //sets one of the room 1 puzzles to false so it doesn't constantly change the doors position
                MainCamera.GetComponent<Interactor>().LivrosDoor.transform.position = MainCamera.GetComponent<Interactor>().LivrosDoorClosedPosition;
                MainCamera.GetComponent<Interactor>().isLivrosDoorOpen = false;
            }
        }
        if (other.gameObject.CompareTag("LabDoorCheck")) {
            if (MainCamera.GetComponent<Interactor>().isLabDoorOpen) {
                Debug.Log("Labyrinth Door Closed");
                //close Labyrinth Door
                MainCamera.GetComponent<Interactor>().LabDoor.transform.rotation = MainCamera.GetComponent<Interactor>().LabDoorInitialRotation;
                MainCamera.GetComponent<Interactor>().isLabDoorOpen = false;
            }
        }

    }



    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if(item != null)
        {
            Hud.CloseMessagePanel();

            mItemToPickup = null;
        }
    }



    // event handler for item removed
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e) {
        IInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        // Detach item from players hand
        goItem.transform.parent = null;
    }

    // event handler for item used
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e) {
        IInventoryItem item = e.Item;

        // Do something with the item... e.g. put it in the hand of the player
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        // Put item on players hand
        goItem.transform.parent = Hand.transform;
    }



    //PAUSE
    public void pause() {
        if (Pause_Canvas.activeInHierarchy) { //Canvas is active
            
            Pause_Canvas.SetActive(false);

            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        else { //Pause menu ctivated
            Time.timeScale = 0;

            Pause_Canvas.SetActive(true);

            //Unlock Cursor
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
    }
} 