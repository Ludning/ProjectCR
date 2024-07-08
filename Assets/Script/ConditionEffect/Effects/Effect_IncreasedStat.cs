using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_IncreasedStat : EffectModule
{
    private Mediator _mediator;

    [SerializeField, ReadOnly]
    private IncreasedStatType _type;
    [SerializeField, ReadOnly]
    private int _value;
    
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
                    _type = Enum.Parse<IncreasedStatType>(keyValueData.Value);
                    break;
                case "Value":
                    _value = int.Parse(keyValueData.Value);
                    break;
            }
        }
    }

    public override void InvokeEffect()
    {
        if (IsActive == false)
        {
            _mediator.Owner.AddStats(_type, _value);
            IsActive = true;
        }
    }

    public override void CancelEffect()
    {
        if (IsActive == true)
        {
            _mediator.Owner.AddStats(_type, -_value);
            IsActive = false;
        }
    }
}