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
    public List<SkillElementData> SkillElement;
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T data) where T : IParserable
    {
    }
}
