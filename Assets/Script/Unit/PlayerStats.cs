using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    #region Field & Property
    public int Level { get; set; }
    public int MaxMp { get; set; }
    public int Mp { get; set; }
    public float CriticalChance { get; set; }
    public float CriticalMultiplier { get; set; }
    public float HpRegen { get; set; }
    public float MpRegen { get; set; }
    public int Exp { get; set; }
    #endregion
}
