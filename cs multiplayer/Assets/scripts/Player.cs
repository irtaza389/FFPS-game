using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{

    public float speed;
    public Transform groundDetector;
    public Transform weaponparent;
    public LayerMask Ground;
    public GameObject cameraparent;
    public float jumpForce= 1000f;
    public int maxHealth;


    private Transform ui_Healhtbar;
    private Text ui_Ammo;

    public Camera normlecam;
    private float Basefov;
    private Rigidbody rig;
    private float sprintModifier = 2f;
    private int currentHealth;
    private GameManager manager;
    private Weapon weapon;
    
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        weapon = GetComponent<Weapon>();
        cameraparent.SetActive(photonView.IsMine);
        if (!photonView.IsMine)
        {
            gameObject.layer = 11;
        }
        Basefov = normlecam.fieldOfView;
        rig = GetComponent<Rigidbody>();
        if (photonView.IsMine)
        {
            ui_Healhtbar = GameObject.Find("HUD/Health/healthBar").transform;
            ui_Ammo = GameObject.Find("HUD/ammo/Text").GetComponent<Text>();
            RefreshHealthBar();
            weapon.RefreshAMoo(ui_Ammo);

        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
       
            //inputs axis
            float t_hmove = Input.GetAxisRaw("Horizontal");
            float t_vmove = Input.GetAxisRaw("Vertical");

            //control

            bool sprint = Input.GetKey(KeyCode.LeftShift);
            bool jump = Input.GetKeyDown(KeyCode.Space);

            //states
            bool IsGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, Ground);
            bool Isjumping = jump && IsGrounded;
            bool isSprinting = sprint && t_vmove > 0 && !Isjumping && IsGrounded;


            //jumping
            if (Isjumping) rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (Input.GetKeyDown(KeyCode.U)) TakeDamage(100);

            //Movement
            Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
            t_direction.Normalize();

            float t_adjustedspeed = speed;
            if (isSprinting) t_adjustedspeed *= sprintModifier;

            Vector3 t_targetVelocity = rig.velocity = transform.TransformDirection(t_direction) * t_adjustedspeed * Time.deltaTime;
            t_targetVelocity.y = rig.velocity.y;
            rig.velocity = t_targetVelocity;

            //Field of View
            if (isSprinting)
            {

                normlecam.fieldOfView = Mathf.Lerp(normlecam.fieldOfView, Basefov * sprintModifier, Time.deltaTime * 8f);
            }
            else
            {
                normlecam.fieldOfView = Mathf.Lerp(normlecam.fieldOfView, Basefov, Time.deltaTime * 8f);
            }
            if(currentHealth < maxHealth && currentHealth != 0)
        {
            currentHealth += 1;

            RefreshHealthBar();
           
        }


        

    
    }

    void RefreshHealthBar()
    {
        float t__health_ratio = (float)currentHealth / (float)maxHealth;
        ui_Healhtbar.localScale =Vector3.Lerp(ui_Healhtbar.localScale,  new Vector3(t__health_ratio, 1, 1),Time.deltaTime*8f);
    }


    public void TakeDamage(int p_damage)
    {
        if (photonView.IsMine)
        {
            currentHealth -= p_damage;
            RefreshHealthBar();
            Debug.Log(currentHealth);
            if (currentHealth < 0)
            {
                manager.Spawn();
                PhotonNetwork.Destroy(gameObject);

                
            }

        }
    }
}
