using System;


#region Weapon

#region WeaponData

public enum ConditionType
{
    None,
    Trigger,
    RequestRecordValue,
    RandomChance,
}

public enum EffectType
{
    SetRecordValue,
    SetRecordValueByReference,
    SetRecordDuration,
    IncreasedStat,
    SpawnObject,
    SpawnProjectile,
    ProjectilePushForce,
    ProjectilePushRange,
    ChangeAttackProjectile,
    ProjectileCount,
    ChargeMinAngle,
    ChargeMaxAngle,
    SetDebuffTarget,
    ChangeWeaponDamageType,
    ChangeWeaponDamageTypeByReference,
    CooldownReduction,
}
public enum IncreasedStatType
{
    Damage,
    Speed,
    CriticalChance,
    CriticalMultiplier,
    Stagger,
    ProjectileSpeed,
}
#endregion
[Flags]
public enum WeaponTrigger
{
    None = 0,
    HoldWeapon = 1 << 0,
    FreeWeapon = 1 << 1,
    
    Attack = 1 << 2,
    SubAttack = 1 << 3,
    Aim = 1 << 4,
    EnergyAttack = 1 << 5,
    
    HitEnemy = 1 << 6,
    KillEnemy = 1 << 7,
    KillMonster = 1 << 8,
    KillElite = 1 << 9,
    KillBoss = 1 << 10,
    
    ShotProjectile = 1 << 11,
    OtherWeaponKillEnemy = 1 << 12,
    
    UseSkill = 1 << 13,
    UseSpecial = 1 << 14,
    
    NearEnemyAttack = 1 << 15,
    HasNearEnemy = 1 << 16,
    LastSurvivor = 1 << 17,
    AliveTeamMember = 1 << 18,
    HitOtherEnemy = 1 << 19,
    
    HoldWeaponStaggerEnemy = 1 << 20,
    OwnerHpChanged = 1 << 21,
}

public enum RecordPropertyType
{
    RecordName,
    RecordType,
    RecordLimit,
    Duration,
    RecordResetValue,
}
public enum RecordName
{
    LassoArrow,
    NearbyEnemy,
    NearbyAlly,
}
public enum RecordType
{
    Stack,
    Value,
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
    KeepAway,
    Instability,
    PhysicalVibration,
    PermanentMovement,
    ExecutionRound,
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
public enum StatusAbnormalityType
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

#region Unit
public enum UnitState
{
    Idle,
    Stun,
    Damaged,
    Die,
}


#region Monster
public enum MonsterType
{
    Normal,
    Elite,
    Boss,
}
#endregion
#endregion