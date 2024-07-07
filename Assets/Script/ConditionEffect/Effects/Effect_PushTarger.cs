using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_PushTarger : EffectModule
{
    private Mediator _mediator;

    [SerializeField, ReadOnly] 
    private float _pushForce;
    [SerializeField, ReadOnly] 
    private float _pushRadius;
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
                case "PushForce":
                    _pushForce = float.Parse(keyValueData.Value);
                    break;
                case "PushRange":
                    _pushRadius = float.Parse(keyValueData.Value);
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