using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestCoolTime : MonoBehaviour
{
    private float[] _skillCoolTimes;
    private float _bossHpRatio;
    private float _playerHpRatio;
    private float _playerMpRatio;


    public void OnClickStartCoolTime()
    {
        StartCoolTime().Forget();
        //StartPlayerStatueTest().Forget();
        StartBossHpTest().Forget();

        _playerHpRatio = 0.2f;
        _playerMpRatio = 0.2f;
        PlayerHp_Message msgHp = new PlayerHp_Message()
        {
            HpRatio = _playerHpRatio
        };
        PlayerMp_Message msgMp = new PlayerMp_Message()
        {
            MpRatio = _playerMpRatio
        };
        MessageManager.Instance.InvokeCallback(msgHp);
        MessageManager.Instance.InvokeCallback(msgMp);
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
            }
            if(count>=4)
                return;
            
            SkillPanal_Message msg = new SkillPanal_Message()
            {
                SkillImagePath = null,
                SkillCoolTimeRatio = _skillCoolTimes
            };
            MessageManager.Instance.InvokeCallback(msg);
            await UniTask.Delay(100);
        }
    }
    
    private async UniTaskVoid StartPlayerStatueTest()
    {
        _playerHpRatio = 1f;
        _playerMpRatio = 1f;
        while (true)
        {
            _playerHpRatio -= 0.01f;
            _playerMpRatio -= 0.01f;
            if (_playerHpRatio <= 0 && _playerMpRatio <= 0)
                return;
            
            PlayerHp_Message msgHp = new PlayerHp_Message()
            {
                HpRatio = _playerHpRatio
            };
            PlayerMp_Message msgMp = new PlayerMp_Message()
            {
                MpRatio = _playerMpRatio
            };
            MessageManager.Instance.InvokeCallback(msgHp);
            MessageManager.Instance.InvokeCallback(msgMp);
            await UniTask.Delay(100);
        }
    }
    private async UniTaskVoid StartBossHpTest()
    {
        _bossHpRatio = 1f;
        while (true)
        {
            _bossHpRatio -= 0.01f;
            if (_bossHpRatio <= 0)
                return;
            
            BossHp_Message msg = new BossHp_Message()
            {
                HpRatio = _bossHpRatio
            };
            MessageManager.Instance.InvokeCallback(msg);
            await UniTask.Delay(100);
        }
    }
}
