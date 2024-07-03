using System;
using System.Collections.Generic;
using System.Data;
using Sirenix.OdinInspector;

[Serializable]
public class ItemData : IParserable
{
    [TableColumnWidth(40, false)]
    public int index;
    public string itemName;
    public string description;
    public string archetype;
    public string specificityRoll;
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T data) where T : IParserable
    {
    }
}