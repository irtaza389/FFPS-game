using UnityEngine;
using MLAgents;
using System;


public class fpsagent : Agent
{

    /* 
     [Tooltip("How fast the agent moves forward")]
     public float moveSpeed = 5f;

     [Tooltip("How fast the agent turns")]
     public float turnSpeed = 180f;

     [Tooltip("Prefab of the heart that appears when the baby is fed")]
     public GameObject heartPrefab;

     [Tooltip("Prefab of the regurgitated fish that appears when the baby is fed")]
     public GameObject regurgitatedFishPrefab;*/

    public enum Team
    {
        Blue = 0,
        Purple = 1
    }
    [HideInInspector]
    public Team team;
    
    int m_PlayerIndex;
    

    [HideInInspector]
    public Rigidbody agentRb;
    tdmSetting playersettings;
    BehaviorParameters m_BehaviorParameters;
    Vector3 m_Transform;
    public float moveSpeed = 25f;
    public float timer = 5f;


    public float health = 100f;
    public GameObject bulletspot;
    public Transform _bullet;
    public string target;
    public GameObject bulletpref;
    public Camera cam;
    


    public override void InitializeAgent()
    {
        base.InitializeAgent();
        m_BehaviorParameters = gameObject.GetComponent<BehaviorParameters>();
        if (m_BehaviorParameters.m_TeamID == (int)Team.Blue)
        {
            team = Team.Blue;
            m_Transform = new Vector3(transform.position.x - 4f, .5f, transform.position.z);
            target = "Purple";
        }
        else
        {
            team = Team.Purple;
            m_Transform = new Vector3(transform.position.x + 4f, .5f, transform.position.z);
            target = "Blue";
        }
       playersettings = FindObjectOfType<tdmSetting>();
        agentRb = GetComponent<Rigidbody>();
        agentRb.maxAngularVelocity = 500;

    }






    public void MoveAgent(float[] act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;




        
        var forwardAxis = (int)act[0];
        var rightAxis = (int)act[1];
        var rotateAxis = (int)act[2];
        

        switch (forwardAxis)
        {
            case 1:
                dirToGo = transform.forward * 1f;
               
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
        }

        switch (rightAxis)
        {
            case 1:
                dirToGo = transform.right * 0.3f;
                break;
            case 2:
                dirToGo = transform.right * -0.3f;
                break;
        }

        switch (rotateAxis)
        {
            case 1:
                rotateDir = transform.up * -1f;
                break;
            case 2:
                rotateDir = transform.up * 1f;
                break;
        }
       
       

        transform.Rotate(rotateDir, Time.deltaTime * 100f);
        agentRb.MovePosition(transform.position + dirToGo * moveSpeed * Time.fixedDeltaTime);
    }
    public override void AgentAction(float[] vectorAction)
    {
        // Existential penalty for strikers.
        AddReward(-1f / 3000f);
        MoveAgent(vectorAction);
       
        
    }




    /* public override void AgentAction(float[] vectorAction)
     {
         // Convert the first action to forward movement
         float forwardAmount = vectorAction[0];
         float jumping = 0f;

         // Convert the second action to turning left or right
         float turnAmount = 0f;
         if (vectorAction[1] == 1f)
         {
             turnAmount = -1f;
         }
         else if (vectorAction[1] == 2f)
         {
             turnAmount = 1f;
         }
         if (vectorAction[2] == 1f)
         {
             jumping = 1f;
         }
         if (vectorAction[3] == 1f)
         {
             Shoot();
         }



         // Apply movement
         rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
         transform.Rotate(transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime);
         rigidbody.AddForce(Vector3.up * jumping * 2f, ForceMode.Impulse);

         // Apply a tiny negative reward every step to encourage action
         if (maxStep > 0) AddReward(-1f / maxStep);
     }*/

    private void Shoot()
    {



        _bullet = Instantiate(bulletpref.transform, bulletspot.transform.position, Quaternion.identity);
        AddReward(0.1f);

       

            
        
        
        
    }

    public override float[] Heuristic()
    {
        var action = new float[4];
        //forward
        if (Input.GetKey(KeyCode.W))
        {
            action[0] = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            action[0] = 2f;
        }
        //rotate
        if (Input.GetKey(KeyCode.A))
        {
            action[2] = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            action[2] = 2f;
        }
        //right
        if (Input.GetKey(KeyCode.E))
        {
            action[1] = 1f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            action[1] = 2f;
        }
      /*  if(Input.GetKey(KeyCode.Mouse0))
        {
            action[3] = 1f;
        }
        */
        
        return action;
    }



    public override void AgentReset()
    {
        if (team == Team.Purple)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        transform.position = m_Transform;
        agentRb.velocity = Vector3.zero;
        agentRb.angularVelocity = Vector3.zero;
        
       
    }


    private void TakeDamage()
    {
        health -= 10f;
        if (health > 0f)
        {

            AddReward(-0.1f);
            RequestAction();
        }
        else if(health<=0f)
        {
            AddReward(-1f);
          /*  RequestDecision();
            gameObject.SetActive(false);*/
            AgentReset();
        }
        
        
    }
    private void OnCollisionEnter(Collision c)
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit , 100f))
        {
            Debug.Log(hit.transform.name);
            
        }
        if (c.gameObject.CompareTag( "bullet"))
        {
            Debug.Log("damanging ");
            AddReward(0.5f);
            TakeDamage();
        }
        if(c.collider.tag==target)
        {
            Debug.Log(c.collider.tag);
            Shoot();
        }
        if(c.collider.gameObject.CompareTag("wall"))
        {
            AddReward(-1f);
            RequestAction();
        }
        if (c.collider.CompareTag("ground"))
        {
            

        }
        else
        {
            health = 0;
            TakeDamage();
        }
    
    }
    private void FixedUpdate()
    {
       
        if (GetStepCount() % 5 == 0)
        {
            RequestDecision();
        }
        else
        {
            RequestAction();
        }
        


    }
    private void Update()
    {
       
    }


}
