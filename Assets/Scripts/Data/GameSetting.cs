using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSetting
{
    //그래픽 설정
    public Resolution Resolution;
    public bool FullScreen;
    public bool AntiAliasing;
    public bool VerticalSync;
    public int FrameLimit;
    
    //오디오 설정
    public int MasterVolume;
    public int MusicVolume;
    public int EffectVolume;
    public bool VolumeActive;

    //게임 플레이 설정
    public Language Language;

    //컨트롤 설정
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Attack;
    public KeyCode Aiming;
    public KeyCode Reload;
    public KeyCode Interaction;

    public void UserFirstSetting()
    {
        Resolution = new Resolution {width = 1280, height = 820};
        FullScreen = true;
        AntiAliasing = true;
        VerticalSync = true;
        FrameLimit = 60;

        MasterVolume = 100;
        MusicVolume = 100;
        EffectVolume = 100;
        VolumeActive = true;

        Language = Language.Korean;

        MoveUp = KeyCode.W;
        MoveDown = KeyCode.S;
        MoveLeft = KeyCode.A;
        MoveRight = KeyCode.D;
        Attack = KeyCode.Mouse0;
        Aiming = KeyCode.Mouse1;
        Reload = KeyCode.R;
        Interaction = KeyCode.F;
    }
}

public enum Language
{
    Korean,
    English,
}
