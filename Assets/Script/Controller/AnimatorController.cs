using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;


    public void SetAnimationClipByWeaponType(StateType stateType, WeaponType weaponType)
    {
        if (weaponType == WeaponType.Common)
            return;
        
        AnimatorOverrideController animatorOverrideController = ResourceManager.Instance.LoadResource<AnimatorOverrideController>(AssetAddressType.WeaponAnimationClipAsset, weaponType.ToString());
        
        //overrideController[stateType.ToString()] = clip;

        animator.runtimeAnimatorController = animatorOverrideController;
    }
    public void SetAnimationClipBySkill()
    {
        
    }

    /*private void ChangeClip()
    {
        AnimationClip newClip = Resources.Load<AnimationClip>("Path/To/NewAnimationClip");
        overrideController["StateName"] = newClip;
    }*/
}