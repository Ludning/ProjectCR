using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SetDebuffTarget : EffectModule
{
    private Mediator _mediator;

    [SerializeField, ReadOnly]
    private DebuffType _type;
    [SerializeField, ReadOnly]
    private string _target;
    public override void InitData(string effectData, Mediator mediator)
    {
        _mediator = mediator;
        
        string data = StringParserHelper.ParenthesesParser(effectData);
        List<string> dataStrings = StringParserHelper.PipeParser(data);
        foreach (var dataString in dataStrings)
        {
            var keyValueData = StringParserHelper.FirstColonParser(dataString);
            switch (keyValueData.Key)
            {
                case "Type":
                    _type = Enum.Parse<DebuffType>(keyValueData.Value);
                    break;
                case "Target":
                    _target = keyValueData.Value;
                    break;
            }
        }
    }

    public override void InvokeEffect()
    {
        
    }

    public override void CancelEffect()
    {
        
    }
}