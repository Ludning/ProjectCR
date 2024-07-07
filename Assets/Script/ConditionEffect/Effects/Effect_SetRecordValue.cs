using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SetRecordValue : EffectModule
{
    private Mediator _mediator;
    
    [SerializeField, ReadOnly]
    private string _recordName;
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
                case "RecordName":
                    _recordName = keyValueData.Value;
                    break;
                case "Value":
                    _value = int.Parse(keyValueData.Value);
                    break;
            }
        }
    }

    public override void InvokeEffect()
    {
        _mediator.SetRecordData(_recordName, _value, RecordDataType.Value);
    }

    public override void CancelEffect()
    {
        
    }
}