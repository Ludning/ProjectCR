using System;
using System.Collections.Generic;
using System.Data;
using Sirenix.OdinInspector;

[Serializable]
public class PlayerData : IParserable
{
    [TableColumnWidth(40)]
    [VerticalGroup("Player Account"), LabelWidth(70)]
    public string identification;
    [VerticalGroup("Player Account"), LabelWidth(70)]
    public string password;
    [VerticalGroup("Player Account"), LabelWidth(70)]
    public string nickname;
    
    [TableColumnWidth(80)]
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string character;
    [VerticalGroup("Player Data"), LabelWidth(70)]
    public int level;
    [VerticalGroup("Player Data"), LabelWidth(70)]
    public int exp;
    
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string inventory_data;
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string equipment_data;
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string ownedSkill_data;
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string equipmentSkill_data;

    public void SetParserData(Dictionary<string, int> columnTypeDic, DataRow dataRow)
    {
        throw new NotImplementedException();
    }
}