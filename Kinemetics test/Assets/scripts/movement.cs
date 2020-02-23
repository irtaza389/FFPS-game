using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{


    public KeyCode left;
    public KeyCode Right;
    public KeyCode forward;
    public KeyCode Backward;







    [SerializeField]
    private float MoveSpeed = 50;

    private Rigidbody body;
    private Animator anim;
    public GameObject cam;
    public Transform head;
    public Transform Headlock;
    public Vector3 headOfSet;
    public float animationspeed;
    //public Transform Gunholdone;




    void Start()
    {

        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
       // cam = gameObject.transform.GetChild(0).gameObject;
        
        
            
        

    }
    private void LateUpdate()
    {
        Quaternion rot = Quaternion.Euler(cam.transform.eulerAngles.x, Headlock.transform.eulerAngles.y, Headlock.transform.eulerAngles.z) * Quaternion.Euler(headOfSet);

        head.transform.rotation = rot;
    }

    void Update()
    {

        
        
            float X = Input.GetAxis("Mouse X");
            float Y = Input.GetAxis("Mouse Y");
            if (Input.GetKey(left))
            {
                anim.SetFloat("Move", Mathf.Lerp(anim.GetFloat("Move"), .5f, animationspeed * Time.deltaTime));
                body.AddRelativeForce(Vector3.left * MoveSpeed * Time.deltaTime, ForceMode.Impulse);
            }
            if (Input.GetKeyUp(left))
            {
                anim.SetFloat("Move", 0);
            }
            if (Input.GetKey(Right))
            {
                anim.SetFloat("Move", Mathf.Lerp(anim.GetFloat("Move"), .5f, animationspeed * Time.deltaTime));
                body.AddRelativeForce(Vector3.left * -MoveSpeed * Time.deltaTime, ForceMode.Impulse);
            }
            if (Input.GetKeyUp(Right))
            {
                anim.SetFloat("Move", 0);
            }

            if (Input.GetKey(forward))
            {
                anim.SetFloat("Move", Mathf.Lerp(anim.GetFloat("Move"), .5f, animationspeed * Time.deltaTime));
                body.AddRelativeForce(Vector3.forward * MoveSpeed * Time.deltaTime, ForceMode.Impulse);
            }
            if (Input.GetKeyUp(forward))
            {
                anim.SetFloat("Move", 0);
            }
            if (Input.GetKey(Backward))
            {
                anim.SetFloat("Move", Mathf.Lerp(anim.GetFloat("Move"), .5f, animationspeed * Time.deltaTime));
                body.AddRelativeForce(Vector3.forward * -MoveSpeed * Time.deltaTime, ForceMode.Impulse);
            }
            if (Input.GetKeyUp(Backward))
            {
                anim.SetFloat("Move", 0);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("Move", Mathf.Lerp(anim.GetFloat("Move"), 1f, animationspeed * Time.deltaTime));
                body.AddRelativeForce(Vector3.forward * (MoveSpeed + 10) * Time.deltaTime, ForceMode.Impulse);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetFloat("Move", 0);
            }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsJumping",true);
            body.AddRelativeForce(Vector3.up * 1000f * Time.deltaTime, ForceMode.Acceleration);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("IsJumping", false);
        }

        gameObject.transform.Rotate(new Vector3(0, X, 0));
            cam.transform.Rotate(new Vector3(-Y, 0, 0));
        }
    }



