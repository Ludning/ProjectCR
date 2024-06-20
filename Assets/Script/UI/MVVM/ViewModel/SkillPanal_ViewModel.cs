using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanal_ViewModel : ViewModelBase
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
    
    
    public override void RegisterEventsOnEnable()
    {
        MessageManager.Instance.RegisterCallback<SkillPanalMessage>(OnResponseSkillPanelData);
    }
    public override void UnRegisterEventsOnDisable()
    {
        MessageManager.Instance.UnRegisterCallback<SkillPanalMessage>();
    }

    private void OnResponseSkillPanelData(SkillPanalMessage message)
    {
        SkillImagePaths = message.SkillImagePath;
        SkillCoolTimeRatios = message.SkillCoolTimeRatio;
    }
}
