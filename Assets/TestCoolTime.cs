using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestCoolTime : MonoBehaviour
{
    private float[] _skillCoolTimes;


    public void OnClickStartCoolTime()
    {
        StartCoolTime().Forget();
    }

    private async UniTaskVoid StartCoolTime()
    {
        _skillCoolTimes = new[]
        {
            1f, 1f, 1f, 1f
        };
        int count = 0;
        while (true)
        {
            for (int i = 0; i < _skillCoolTimes.Length; i++)
            {
                _skillCoolTimes[i] -= 0.01f;
                if (_skillCoolTimes[i] <= 0)
                {
                    _skillCoolTimes[i] = 0;
                    count++;
                }
                //Debug.Log($"index : {i}, value : {_skillCoolTimes[i]}");
            }
            if(count>=4)
                return;
            
            SkillPanalMessage msg = new SkillPanalMessage()
            {
                SkillImagePath = null,
                SkillCoolTimeRatio = _skillCoolTimes
            };
            MessageManager.Instance.InvokeCallback(msg);
            await UniTask.Delay(100);
        }
    }
}
