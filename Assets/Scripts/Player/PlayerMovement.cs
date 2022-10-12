using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool debugMode = false;
    public CharacterController controller;
    public float walkSpeed = 5;
    public float sprintSpeed = 7;
    public float slideSpeed = 9;
    private float currentSpeed;
    private float currentSlideSpeed;
    public float fallSpeed = 1f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 2f;
    public float gravity = -29.81f;
    public Camera mainCamera;
    public Vector3 velocity;
    public bool sliding = false;
    Vector3 direction;
    public Transform normalCamPos;
    public Transform crouchCamPos;
    private float crouchTime = 0;
    public float slideViewSpeed = 10;
    public bool toggleSprint = false;
    public bool sprintToggle = false;
    private float referenceY;
    public float wallRunFallOff = 1;
    public bool movingAllowed = true;
    // Start is called before the first frame update

    void Start()
    {
        currentSpeed = walkSpeed;
        currentSlideSpeed = slideSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Walking Code
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        if (sliding == false)
        {
            direction = transform.right * xAxis + transform.forward * zAxis;
        }
        if (movingAllowed == true)
        {
            controller.Move(direction * Time.deltaTime * currentSpeed);
        }
        
        //Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Jumping Code
        if (isGrounded && velocity.y < 0)

        {
            velocity.y = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            DebugMsg("jumped");

        }
        velocity.y += gravity * Time.deltaTime;
        if (movingAllowed == true)
        {
            controller.Move(velocity * Time.deltaTime);
        }
        
        //Running code
        if (Input.GetKeyDown(KeyCode.LeftShift) && toggleSprint == false)
        {
            currentSpeed = sprintSpeed;
            mainCamera.fieldOfView = 60 + 0.2f*(sprintSpeed-walkSpeed);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && toggleSprint == false)
        {
            currentSpeed = walkSpeed;
            mainCamera.fieldOfView = 60;
        }
        if (toggleSprint == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sprintToggle = !sprintToggle;
            }
            if (sprintToggle == true)
            {
                currentSpeed = sprintSpeed;
                mainCamera.fieldOfView = 60 + 0.2f*(sprintSpeed-walkSpeed);
            }
            if (sprintToggle != true)
            {
                currentSpeed = walkSpeed;
                mainCamera.fieldOfView = 60;
            }

        }
        //Anti Slope Bounc test
        if (isGrounded == true)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                velocity.y = -10;
            }
        }

        


        //Combat slide
        mainCamera.transform.position = Vector3.Lerp(normalCamPos.position, crouchCamPos.position, crouchTime);
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            crouchTime = Mathf.Clamp(crouchTime - Time.deltaTime*slideViewSpeed,0,1);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouchTime = Mathf.Clamp(crouchTime + Time.deltaTime * slideViewSpeed, 0, 1);
            if (isGrounded == true)
            {
                if (velocity.y >= -10)
                {
                    currentSlideSpeed = Mathf.Clamp(currentSlideSpeed - Time.deltaTime * 10, 0, slideSpeed);
                }
                if (velocity.y < -10)
                {
                    currentSlideSpeed += Time.deltaTime * 2;
                }

                //if (!Input.GetKey(KeyCode.Space))
                // {
                //    velocity.y = -10;
                // }

            }
            if (isGrounded == false)
            {
                currentSlideSpeed += Time.deltaTime * 2;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentSlideSpeed -= 1;
            }
                
            currentSpeed = currentSlideSpeed;
            
        }
  
        if (sliding == false)
        {
            currentSlideSpeed = Mathf.Clamp(currentSlideSpeed + Time.deltaTime*11,0,slideSpeed);

        }
        //Camera Slide Move
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            DebugMsg("GOD D*** IT");
            sliding = false;
            currentSpeed = sprintSpeed;
            mainCamera.transform.position = normalCamPos.position;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            sliding = true;
            mainCamera.transform.position = crouchCamPos.position;

        }
        //Debug Message
        void DebugMsg(string message)
        {
            if (debugMode == true)
            {
                Debug.Log(message);
            }
        }
        //Wallrun
        RaycastHit hit;
        if (debugMode == true)
        {
            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.right * 1f, Color.red, 0.1f);
            Debug.DrawRay(mainCamera.transform.position, -mainCamera.transform.right * 1f, Color.blue, 0.1f);
        }
      
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.right, out hit,1f) || Physics.Raycast(mainCamera.transform.position, -mainCamera.transform.right, out hit, 1f))
        {
            if (hit.transform.tag == "Ground" && Input.GetKey(KeyCode.W) && isGrounded == false)
            {
                DebugMsg("side cast hit ground");
                gravity = -wallRunFallOff;
                velocity.y = Mathf.Clamp(velocity.y, -100, 1f);
                currentSpeed = sprintSpeed + 2f;
            }
            
        }
        else
        {
            gravity = -29.81f; 
        }
    }
   
}
