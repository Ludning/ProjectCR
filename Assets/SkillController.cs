using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private Ability _normalAttack;
    private Ability _util;
    private Ability _skill;
    private Ability _special;

    public void SetSkill(SkillSlotType type, int skillID)
    {
        GameData gameData = DataManager.Instance.GetGameData();
        if (!gameData.SkillData.TryGetValue(skillID, out SkillData data))
            return;
        switch (type)
        {
            case SkillSlotType.Normal:
                _normalAttack.InitData(data);
                break;
            case SkillSlotType.Util:
                _util.InitData(data);
                break;
            case SkillSlotType.Skill:
                _skill.InitData(data);
                break;
            case SkillSlotType.Special:
                _special.InitData(data);
                break;
        }
    }
}
