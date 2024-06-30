
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
public enum MonsterInfoUIType
{
    Monster,
    Elite,
}
public enum PopupUIElementType
{
    SkillTreeUI,
    InventoryUI,
    EquipmentUI,
}

public enum PrefabType
{
    PopupUIAsset,
    CharacterAsset,
    AnimalAsset
}

public enum ItemType
{
    Common,
    Blade,
    Bow,
    Staff,
}

public enum SpecificityType
{
    Test,
}

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

public enum DetectionRangeType
{
    Circle,
    SemiCircle,
}

public enum AssetAddressType
{
    GameUIAsset,
    PopupUIAsset,
    CharacterAsset,
    MonsterUIAsset,
    AnimalAsset,
}

public enum UnitState
{
    Idle,
    Stun,
    Damaged,
    Die,
}