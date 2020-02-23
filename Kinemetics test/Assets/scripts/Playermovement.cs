using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.87f;
    public float jumpHeight = 10f;
    public float time = 3f;
    Vector3 Velocity = Vector3.zero;
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;
    bool isGrounded;
    bool isMoving;
    public Animator anim;
    private Rigidbody rb;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundmask);
        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }


            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        

         if(Input.GetButtonDown("Jump") && isGrounded)
        {
            
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }
}
