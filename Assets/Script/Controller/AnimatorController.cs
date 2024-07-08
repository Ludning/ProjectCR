using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator UnitAnimator;
    [SerializeField] private AnimatorOverrideController overrideController;


    public void SetAnimationClipByWeaponType(StateType stateType, WeaponType weaponType)
    {
        AnimationClip clip = null;
        switch (weaponType)
        {
            case WeaponType.Blade:
                clip = ResourceManager.Instance.LoadResource<AnimationClip>(AssetAddressType.WeaponAnimationClipAsset, "Blade");
                break;
            case WeaponType.Bow:
                clip = ResourceManager.Instance.LoadResource<AnimationClip>(AssetAddressType.WeaponAnimationClipAsset, "Bow");
                break;
            case WeaponType.Staff:
                clip = ResourceManager.Instance.LoadResource<AnimationClip>(AssetAddressType.WeaponAnimationClipAsset, "Staff");
                break;
        }

        if (clip == null)
            return;

        switch (stateType)
        {
            case StateType.Attack:
                break;
            case StateType.SubAttack:
                break;
            case StateType.Aim:
                break;
            case StateType.Skill:
                break;
            case StateType.Special:
                break;
        }
    }
    public void SetAnimationClipBySkill()
    {
        
    }

    private void ChangeClip()
    {
        AnimationClip newClip = Resources.Load<AnimationClip>("Path/To/NewAnimationClip");
        overrideController["StateName"] = newClip;
    }
}