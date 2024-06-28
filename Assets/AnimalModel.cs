using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalModel : MonoBehaviour
{
    void OnBecameVisible()
    {
        UIManager.Instance.ShowMonsterUIElement(gameObject, MonsterInfoUIType.Monster);
    }
    void OnBecameInvisible()
    {
        
    }
}
