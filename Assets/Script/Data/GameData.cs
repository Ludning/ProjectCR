using System;
using System.Collections;
using System.Collections.Generic;
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
    [TableList] public Dictionary<string, PlayerData> PlayerData;
    [TableList] public Dictionary<string, MonsterData> MonsterData;
    [TableList] public Dictionary<string, EliteData> EliteData;
    [TableList] public Dictionary<string, BossData> BossData;
    [TableList] public Dictionary<int, ArchetypeData> ArchetypeData;
    [TableList] public Dictionary<int, SpecificityData> SpecificityData;
}

[Serializable]
public class LevelData
{
    [TableColumnWidth(40, false)]
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
    public int expRequired;
}
[Serializable]
public class SkillData 
{
    [TableColumnWidth(40, false)]
    public int index;
    public string skillName;
    public string description;
    public List<SkillElement> SkillElement;
}

[Serializable]
public class SkillElement
{
    public SkillType skillType;
    public SkillLogicType skillLogicType;
    public DamageType damageType;
    public EffectType effectType;
    public BuffType buffType;
    public CostType costType;
    public float skillValue;
    public float skillRange;
    public float costValue;
    public TargetType targetType;
    public TargetingType targetingType;
    public int targetingCount;
}


[Serializable]
public class ItemData 
{
    [TableColumnWidth(40, false)]
    public int index;
    public string itemName;
    public string description;
    public string archetype;
    public string specificityRoll;
}
[Serializable]
public class PlayerData
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
}
[Serializable]
public class MonsterData
{
    public string monsterName;
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
}
[Serializable]
public class EliteData
{
    public string monsterName;
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
}
[Serializable]
public class BossData
{
    public string monsterName;
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
}

[Serializable]
public class ArchetypeData
{
    [TableColumnWidth(40, false)]
    public int index;
    public ArchetypeType archetypeType;
    public string name;
    public string description;
    public WeaponType weaponType;
    public string archetypeEffect;
}
[Serializable]
public class SpecificityData
{
    [TableColumnWidth(40, false)]
    public int index;
    public SpecificityType specificityType;
    public string name;
    public string description;
    public string specificityEffect;
}

/*
Zebra
Wombat
Wolf
Wildebeest
Walrus
Turkey
Toucan
Tortoise
Tiger
TasmanianDevil
Tapir
Swan
Squirrel
Squid
SnowWeasel
SnowOwl
SnowLeopard
SnappingTurtle
Snake
Sloth
Skunk
Sheep
Serval
SeaOtter
Seal
Seahorse
Rooster
Rhino
Reindeer
RedPanda
Rattlesnake
Raccoon
Rabbit
Quokka
Python
Puffin
Pronghorn
Possum
PolarBear
Platypus
Pigeon
Pig
Penguin
Pelican
Peacock
Parrot
Panda
Ox
Owl
Ostrich
Orca
Narwhal
Moose
Monkey
Mole
Mice
Marten
Manatee
Mallard
Lynx
Lobster
Llama
Lioness
Lion
Lemur
Lamb
Kookaburra
Koala
Kingfisher
Kangaroo
Jackal
Iguana
Hyena
Husky
Horse
HornedLizard
Hornbill
Hog
Hippo
Hen
Hedgehog
Hare
Hamster
Gorilla
Goose
Goldfish
GoldenEagle
Goat
Giraffe
GilaMonster
Gazelle
Frog
Fox
Flamingo
Ferret
Emu
Elephant
Eagle
Duck
Dove
Donkey
Dolphin
Dog
Deer
Crow
Crocodile
Crab
Coyote
Cow
Cougar
Cobra
Clownfish
Chipmunk
Chick
Cheetah
Chameleon
Cat
Carp
Camel
Bull
Buffalo
Bison
Bighorn
Beluga
Beaver
Bear
Bat
Badger
Baboon
Arowana
Armadillo
ArcticFox
Antelope
Alpaca
     */

