using System;


#region Weapon
[Flags]
public enum WeaponCondition
{
    None = 0,
    HoldWeapon = 1 << 0,
    FreeWeapon = 1 << 1,
    
    Attack = 1 << 2,
    EnergyAttack = 1 << 3,
    
    HitEnemy = 1 << 4,
    KillEnemy = 1 << 5,
    KillMonster = 1 << 6,
    KillElite = 1 << 7,
    KillBoss = 1 << 8,
    
    ShotProjectile = 1 << 9,
    OtherWeaponKillEnemy = 1 << 10,
}

public enum RecordName
{
    LassoArrow,
    NearbyEnemy,
    NearbyAlly,
}
public enum RecordType
{
    None,
    Stack,
    Value,
}

public enum WeaponEffectType
{
    IncreasedStat,
    
}
public enum WeaponIndexType
{
    Primary,
    Secondary,
}
public enum WeaponType
{
    Common,
    Blade,
    Bow,
    Staff,
}

public enum ComparisonType
{
    More,
    Over,
    Below,
    Under,
    Same,
}
public enum ArchetypeType
{
    Adaptive,
    Aggressive,
    LightWeight,
    Precision,
    Heavy,
    Shadow,
    Predation,
    Hunter,
    Lasso,
    Cleavage,
    Conversion,
}
public enum SpecificityType
{
    Discord,
    AvoidAccess,
    Instability,
    PhysicalVibration,
    PermanentMovement,
    CloseRangeExecution,
    Tombstone,
    Ensemble,
    InfiniteSadness,
    ChainReaction,
    OneForAll,
    Fluctuation,
    OsmoticPower,
    Meganeura,
    Onslaught,
    DegradableDestructionDevice,
    RangeSecuringDevice,
    Preparation,
    Pressure,
    Wildcard,
}
#endregion

#region InputAction
public enum InputActionMapType
{
    Player,
}
public enum InputActionType
{
    Move,
    Run,
    Jump,
    Attack,
    Util,
    Interaction,
    Skill,
    Special,
}
#endregion
#region PrefabType
public enum PrefabType
{
    PopupUIAsset,
    CharacterAsset,
    AnimalAsset
}
#endregion
#region UIType
public enum UICoreType
{
    Canvas,
    EventSystem,
}
public enum GameUIElementType
{
    PlayerHpBar,
    PlayerMpBar,
    SkillPanel,
    InventoryButton,
    MenuButton,
    BuffInfo,
    BossHpBar,
}
public enum MiddleUIType
{
    MonsterInfo,
    PlayerInfo,
}
public enum PopupUIElementType
{
    SkillTreeUI,
    InventoryUI,
    EquipmentUI,
}
#endregion
#region Skill
public enum SkillType
{
    Active,
    Passive,
}
public enum ProjectileType
{
    NonProjectile,
    Projectile,
}
public enum SkillLogicType
{
    Test,
}
public enum DamageType
{
    Kinetic,
    Stasis,
    Strand,
    Arc,
    Solar,
    Void,
}
public enum EffectType
{
    None,
    Incineration,//소각
    Bleeding,    //출혈
    Poisoning,   //중독
    //단절
    //빙결
    //
}
public enum BuffType
{
    Buff,
    Debuff,
}
public enum CostType
{
    None,
    Hp,
    Mp,
}
public enum TargetType
{
    Enemy,
    Friendly,
}
public enum TargetingType
{
    AutoTargeting,
    NonTargeting,
    None,
}
public enum DamageCalculateType
{
    
}
#endregion
#region Address
public enum AssetAddressType
{
    UICoreAsset,
    GameUIAsset,
    PopupUIAsset,
    CharacterAsset,
    MiddleUIAsset,
    AnimalAsset,
    WeaponAsset,
    WeaponAnimationClipAsset,
    SkillAnimationClipAsset,
}
#endregion
#region State
public enum UnitState
{
    Idle,
    Stun,
    Damaged,
    Die,
}
#endregion
