using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo_View : ViewBase<MonsterInfo_ViewModel, MonsterInfo_Message>, ISetAbleMiddleViewId
{
    [SerializeField] private RectTransform UITransform;
    [SerializeField] private Slider MonsterHpSlider;

    public void SetId(int id)
    {
        _vm.ID = id;
    }
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.HpRatio):
                MonsterHpSlider.value = _vm.HpRatio;
                break;
        }
    }
}
