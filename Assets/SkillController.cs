using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    private Player owner;
    [SerializeField]
    private Ability _normalAttack;
    [SerializeField]
    private Ability _util;
    [SerializeField]
    private Ability _skill;
    [SerializeField]
    private Ability _special;

    public void SetSkill(SkillSlotType type, int skillID)
    {
        GameData gameData = DataManager.Instance.GetGameData();
        if (!gameData.SkillData.TryGetValue(skillID, out SkillData data))
            return;
        switch (type)
        {
            case SkillSlotType.Normal:
                _normalAttack.InitData(data, owner);
                break;
            case SkillSlotType.Util:
                _util.InitData(data, owner);
                break;
            case SkillSlotType.Skill:
                _skill.InitData(data, owner);
                break;
            case SkillSlotType.Special:
                _special.InitData(data, owner);
                break;
        }
    }
}
