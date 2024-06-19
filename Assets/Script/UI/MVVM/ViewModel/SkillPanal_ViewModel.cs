using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanal_ViewModel : ViewModelBase
{
    public string[] SkillImagePath;
    public float[] SkillCoolTimeRatio;
    
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
        SkillImagePath = message.SkillImagePath;
        SkillCoolTimeRatio = message.SkillCoolTimeRatio;
    }
}
