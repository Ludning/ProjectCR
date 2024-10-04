using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeData
{
    [SerializeField]
    private List<int> _ownedSkillId = new List<int>();
    [SerializeField]
    private List<int> _equipmentSkillId = new List<int>();

    public void LoadData(string ownedSkillString, string equipmentSkillString)
    {
        LoadSkills(ownedSkillString, _ownedSkillId);
        LoadSkills(equipmentSkillString, _equipmentSkillId);
    }

    private void LoadSkills(string skillString, List<int> skillList)
    {
        skillString = skillString.Trim('{', '}');
        string[] skillIdArr = skillString.Split(',');

        foreach (string skillId in skillIdArr)
        {
            if (int.TryParse(skillId, out int number))
            {
                skillList.Add(number);
            }
            else
            {
                Debug.LogError("스킬 데이터 파싱 실패");
            }
        }
    }
                
}
