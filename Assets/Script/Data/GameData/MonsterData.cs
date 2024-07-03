using System;
using System.Collections.Generic;
using System.Data;

[Serializable]
public class MonsterData : IParserable
{
    public string monsterName;
    public MonsterType MonsterType;
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T data) where T : IParserable
    {
    }
}
