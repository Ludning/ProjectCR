using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
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
    
    [TableList]
    public List<LevelData> LevelData;
    [TableList]
    public List<SkillData> SkillData;
    [TableList]
    public List<MonsterData> MonsterData;
    [TableList]
    public List<EliteData> EliteData;
    [TableList]
    public List<BossData> BossData;

}

[Serializable]
public struct LevelData
{
    [TableColumnWidth(1)]
    public int level;
    public int hp;
    public int attack;
    public int defence;
    public int expRequired;
}
[Serializable]
public struct SkillData 
{
    [TableColumnWidth(1)]
    public int index;
    public int skillName;
}
[Serializable]
public struct MonsterData
{
    [TableColumnWidth(1)]
    public int index;
    public int monsterID;
    public int hp;
    public int level;
}
[Serializable]
public struct EliteData
{
    [TableColumnWidth(1)]
    public int index;
    public int eliteID;
    public int hp;
    public int level;
}
[Serializable]
public struct BossData
{
    [TableColumnWidth(1)]
    public int index;
    public int bossID;
    public int hp;
    public int level;
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

