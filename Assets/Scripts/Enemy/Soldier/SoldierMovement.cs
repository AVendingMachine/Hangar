using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    private GameObject player;
    public CharacterController controller;
    public bool isGrounded = true;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public LayerMask groundMask;
    public float walkSpeed = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(transform.forward*Time.deltaTime * walkSpeed);
        transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
        //Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Jumping Code
        if (isGrounded && velocity.y < 0)

        {
            velocity.y = -1;
        }
       
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
