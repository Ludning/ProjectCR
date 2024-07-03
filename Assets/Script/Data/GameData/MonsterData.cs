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
    public void SetParserData(Dictionary<string, int> columnTypeDic, DataRow dataRow)
    {
        throw new NotImplementedException();
    }
}
