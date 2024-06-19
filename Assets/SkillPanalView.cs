using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanalView : ViewBase<SkillPanal_ViewModel>
{
    [SerializeField] private List<Image> skillImage;
    [SerializeField] private List<Image> skillCoolTimeSlider;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Debug.Log("OnPropertyChanged");
        switch (e.PropertyName)
        {
            case nameof(_vm.SkillImagePath):
                ChangeSkillIcon(_vm.SkillImagePath);
                break;
            case nameof(_vm.SkillCoolTimeRatio):
                ChangeSkillCoolTime(_vm.SkillCoolTimeRatio);
                break;
        }
    }

    private void ChangeSkillIcon(string[] imgPaths)
    {
        if (imgPaths.IsNullOrEmpty())
            return;
        
        Debug.Log("스킬 이미지 셋업");
        //리소스 매니저에서 이미지 리소스를 불러와 비교,
        //skillImage
    }

    private void ChangeSkillCoolTime(float[] ratios)
    {
        if (ratios.IsNullOrEmpty())
            return;
        if (skillCoolTimeSlider.Count == ratios.Length)
            return;
        
        Debug.Log("스킬 쿨타임 셋업");
        for (int i = 0; i < ratios.Length; i++)
        {
            skillCoolTimeSlider[i].fillAmount = ratios[i];
        }
    }
}
