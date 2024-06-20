using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillPanal_View : ViewBase<SkillPanal_ViewModel, SkillPanal_Message>
{
    [SerializeField] private List<Image> skillImages;
    [SerializeField] private List<Image> skillCoolTimeSliders;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Debug.Log($"OnPropertyChanged");
        switch (e.PropertyName)
        {
            case nameof(_vm.SkillImagePaths):
                ChangeSkillIcon(_vm.SkillImagePaths);
                break;
            case nameof(_vm.SkillCoolTimeRatios):
                ChangeSkillCoolTime(_vm.SkillCoolTimeRatios);
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
        if (skillCoolTimeSliders.Count != ratios.Length)
            return;
        
        Debug.Log("스킬 쿨타임 셋업");
        for (int i = 0; i < ratios.Length; i++)
        {
            skillCoolTimeSliders[i].fillAmount = ratios[i];
        }
    }
}
