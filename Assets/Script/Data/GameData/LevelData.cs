using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class LevelData : IParserable
{
    [TableColumnWidth(40, false)]
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
    public int expRequired;
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T data) where T : IParserable
    {
    }
}
