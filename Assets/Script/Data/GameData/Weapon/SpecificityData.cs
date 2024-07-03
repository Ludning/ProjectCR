using System;
using System.Collections.Generic;
using System.Data;
using Sirenix.OdinInspector;

[Serializable]
public class SpecificityData : IParserable
{
    [TableColumnWidth(40, false)]
    public int index;
    public SpecificityType specificityType;
    public string name;
    public string description;
    public string effect;
    public string record;
    public void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T data) where T : IParserable
    {
        throw new NotImplementedException();
    }
}