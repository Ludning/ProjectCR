using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp_View : ViewBase<PlayerHp_ViewModel, PlayerHp_Message>
{
    [SerializeField] private Slider PlayerHpSlider;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.HpRatio):
                PlayerHpSlider.value = _vm.HpRatio;
                break;
        }
    }
}
