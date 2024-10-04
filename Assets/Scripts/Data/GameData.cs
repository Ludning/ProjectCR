using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : SerializedScriptableObject
{
    //서버에 있어야 하는 데이터
    //레벨당 스펙 데이터
    //몬스터 관련 데이터
    //퀘스트 진행도 데이터
    //아이템 id 데이터
    //스킬 id 데이터
    //캐릭터 힘민지등 스텟 데이터
    
    //클라이언트에 있어야 하는 데이터
    //캐릭터 대화 데이터
    //퀘스트 내용 데이터
    //아이템 데이터
    //스킬 데이터
    
    [TableList] public Dictionary<int, LevelData> LevelData;
    [TableList] public Dictionary<int, SkillData> SkillData;
    [TableList] public Dictionary<int, ItemData> ItemData;
    [TableList] public Dictionary<string, MonsterData> MonsterData;
    [TableList] public Dictionary<int, ArchetypeData> ArchetypeData;
    [TableList] public Dictionary<int, SpecificityData> SpecificityData;
}