using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class BossHp_View : ViewBase<BossHp_ViewModel, BossHp_Message>
{
    [SerializeField] private Slider BossHpSlider;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.HpRatio):
                BossHpSlider.value = _vm.HpRatio;
                break;
        }
    }
}
