using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SetRecordValueByReference : EffectModule
{
    private Mediator _mediator;
    
    [SerializeField, ReadOnly]
    private string _recordName;
    [SerializeField, ReadOnly]
    private ReferenceType _reference;
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
                case "RecordName":
                    _recordName = keyValueData.Value;
                    break;
                case "ReferenceName":
                    _reference = Enum.Parse<ReferenceType>(keyValueData.Value);
                    break;
            }
        }
    }

    public override void InvokeEffect()
    {
        int value = PlayerManager.Instance.GetDataByReferenceName(_reference);
        _mediator.SetRecordData(_recordName, value);
    }

    public override void CancelEffect()
    {
        
    }
}