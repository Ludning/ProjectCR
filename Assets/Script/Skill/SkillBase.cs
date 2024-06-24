using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase
{
    //스킬 효과
    //스킬 방식, 스킬 값, 효과 범위, 효과 지속 시간
    private int _skillType;
    private int _skillValue;
    private int _effectRange;
    private int _effectㅇuration;
    //자원 및 쿨타임
    private int _costType;
    private int _costValue;
    private float _coolTime;
    //스킬 대상
    private int _targetType;
    private int _targetingType;
    //조건 및 제한사항
    /*
    사용 조건: 스킬 사용을 위한 조건 (예: 특정 레벨 이상, 특정 스킬 선행 필요 등).
    사용 제한: 스킬 사용 제한 조건 (예: 특정 상황에서만 사용 가능, 하루 사용 횟수 제한 등).
    시전 시간: 스킬을 발동하는 데 필요한 시간.
    */
    //속성 및 타입
    //private int 
    
    //데미지 및 효과 계산
    private int _damageType;
    
    /*
     부가 효과
     상태 이상: 스킬에 의해 발생할 수 있는 상태 이상 (예: 중독, 기절, 침묵 등).
     추가 효과: 스킬 사용 시 발생하는 부가 효과 (예: 추가 피해, 상태 이상 면역 등).
    */
    
    /*
     상태 관리
     스택 관리: 스킬의 중첩 가능 여부와 중첩 수.
     효과 리셋: 특정 조건에서 효과가 초기화되거나 제거되는지 여부.
    */
    
    /*
     특수 효과 및 상호작용
     스킬 상호작용: 다른 스킬과의 상호작용 (예: 특정 스킬 사용 후 추가 데미지, 콤보 스킬 등).
     지형 및 환경 효과: 특정 지형이나 환경에서 스킬의 효과가 달라질 경우.
    */
}
