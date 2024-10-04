using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine.Serialization;

[Serializable]
public class SkillElementData : IParserable
{
    public SkillType skillType;
    public SkillLogicType skillLogicType;
    public DamageType damageType;
    public StatusAbnormalityType statusAbnormalityType;
    public BuffType buffType;
    public CostType costType;
    public float skillValue;
    public float skillRange;
    public float costValue;
    public TargetType targetType;
    public TargetingType targetingType;
    public int targetingCount;
    public void SetParserData(Dictionary<string, int> columnTypeDic, DataRow dataRow)
    {
        
    }
}