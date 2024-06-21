using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMp_View : ViewBase<PlayerMp_ViewModel, PlayerMp_Message>
{
    [SerializeField] private Slider PlayerMpSlider;
    public float animationDuration = 0.5f;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.MpRatio):
                //PlayerMpSlider.value = _vm.MpRatio;
                PlayerMpSlider.DOKill();
                PlayerMpSlider.DOValue(_vm.MpRatio, animationDuration).SetEase(Ease.OutQuad);
                break;
        }
    }
}
