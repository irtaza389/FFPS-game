using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sway : MonoBehaviour
{


    public float inesity;
    public float smooth;
    public bool IsMine;

    private Quaternion originRotation;


    private void Start()
    {
        originRotation = transform.localRotation;
    }
    private void Update()
    {
      
        
            Updatesway();
          
    }

    private void Updatesway()
    {
        //cpntrols
        float t_xmouse = Input.GetAxis("Mouse X");
        float t_ymouse = Input.GetAxis("Mouse Y");
        if (!IsMine)
        {
             t_xmouse = 0;
             t_ymouse = 0;
        }
        //calculating rotation
        Quaternion t_xadj = Quaternion.AngleAxis(-inesity*t_xmouse, Vector3.up);
        Quaternion t_yadj = Quaternion.AngleAxis(inesity * t_ymouse, Vector3.right);

        Quaternion target_rotation = originRotation * t_xadj*t_yadj;
        //rotate towards the target 
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);

    }
}
   
