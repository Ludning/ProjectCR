using System;
using System.Collections.Generic;
using System.Data;
using Sirenix.OdinInspector;

[Serializable]
public class SkillData : IParserable
{
    [TableColumnWidth(40, false)]
    public int index;
    public string skillName;
    public string description;
    public List<SkillElement> SkillElement;
    public void SetParserData(Dictionary<string, int> columnTypeDic, DataRow dataRow)
    {
        throw new NotImplementedException();
    }
}
