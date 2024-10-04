using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_RequestReference : ConditionModule
{
    private Mediator _mediator;
    
    private string _targetReferenceName;

    public override void InitData(string conditionData, Mediator mediator)
    {
        _mediator = mediator;
        
        string data = StringParserHelper.ParenthesesParser(conditionData);
        List<string> dataStrings = StringParserHelper.PipeParser(data);
        foreach (var dataString in dataStrings)
        {
            var keyValueData = StringParserHelper.FirstColonParser(dataString);
            switch (keyValueData.Key)
            {
                case "ReferenceName":
                    _targetReferenceName = keyValueData.Value;
                    break;
            }
        }

        if (string.IsNullOrWhiteSpace(_targetReferenceName) == true)
            throw new System.NotImplementedException();
    }

    public override bool CheakCondition()
    {
        int data = DataRequest(_targetReferenceName);
        if (data == int.MaxValue)
            return false;
        
        
        return true;
    }
    
    private int DataRequest(string message)
    {
        return _mediator.GetRecordData(message);
    }
}
