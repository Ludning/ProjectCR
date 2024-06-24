using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase
{
    public int index;
    public ItemType itemType;
    public ArchetypeBase Archetype;
    public List<SpecificityBase> Specificity;
}



/*
아이템은 스킬의 효과를 변경할 수 있어야함
3~개의 랜덤 패시브가 존재
각 패시브마다 패시브테이블이 존재해서 테이블에서 랜덤으로 하나가 결정됨
*/