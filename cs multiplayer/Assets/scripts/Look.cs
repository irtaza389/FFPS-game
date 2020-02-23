using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Look : MonoBehaviourPunCallbacks

{
    #region variables
    public static bool cursorLook= true;
    public Transform player;
    public Transform cam;
    public Transform weapon;
    public float xSensitivity;
    public float ySensitivity;
    public float maxAngle;
    private Quaternion camcenter;
    #endregion

    #region monocallback
    void Start()
    {
        camcenter = cam.localRotation;
    }
    void Update()
    {
        if (!photonView.IsMine) return;
        

            setY();
            setX();
            UpdatecusorLock();
        
    }
    #endregion

    #region private


    void setY()
    {
         

        float t_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cam.localRotation * t_adj;

        if (Quaternion.Angle(camcenter, t_delta) < maxAngle)
        {
            cam.localRotation = t_delta;
            
        }
        weapon.localRotation = cam.localRotation;
    }

    void setX()
    {
        float t_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = player.localRotation * t_adj;
      player.localRotation = t_delta;
        
    }
    void UpdatecusorLock()
    {
        if (cursorLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLook = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLook = true;
            }

        }
    }
    #endregion


}

