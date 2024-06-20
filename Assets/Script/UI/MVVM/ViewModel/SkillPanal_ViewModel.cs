using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanal_ViewModel : ViewModelBase<SkillPanal_Message>
{
    private string[] _skillImagePaths;
    private float[] _skillCoolTimeRatios;
    public string[] SkillImagePaths
    {
        get => _skillImagePaths;
        set
        {
            _skillImagePaths = value;
            OnPropertyChanged(nameof(SkillImagePaths));
        }
    }
    public float[] SkillCoolTimeRatios
    {
        get => _skillCoolTimeRatios;
        set
        {
            _skillCoolTimeRatios = value;
            OnPropertyChanged(nameof(SkillCoolTimeRatios));
        }
    }

    protected override void OnResponseMessage(SkillPanal_Message message)
    {
        SkillImagePaths = message.SkillImagePath;
        SkillCoolTimeRatios = message.SkillCoolTimeRatio;
    }
}
