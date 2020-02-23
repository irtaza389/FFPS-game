using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ik : MonoBehaviour
{
    Animator anim;
    public Transform leftiftarget;
    public Transform RightIKtarget;
    public float ikwight = 1;
    public Transform hintLeft;
    public Transform hintright;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAnimatorIK()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikwight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikwight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftiftarget.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, RightIKtarget.position);

  anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, ikwight);
        anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, ikwight);

        anim.SetIKHintPosition(AvatarIKHint.LeftElbow, hintLeft.position);
        anim.SetIKHintPosition(AvatarIKHint.RightElbow, hintright.position);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikwight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikwight);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftiftarget.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightHand, RightIKtarget.rotation);

        





    }
}
