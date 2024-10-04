using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Condition_RequestRecordValue : ConditionModule
{
    private Mediator _mediator;
    
    private string _targetRecordName;
    private ComparisonType _comparisonType;
    private int _comparisonValue;
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
                case "RecordName":
                    _targetRecordName = keyValueData.Value;
                    break;
                case "Comparison":
                    _comparisonType = Enum.Parse<ComparisonType>(keyValueData.Value);
                    break;
                case "Value":
                    _comparisonValue = int.Parse(keyValueData.Value);
                    break;
            }
        }

        if(_mediator == null)
            throw new System.NotImplementedException();
        if (string.IsNullOrWhiteSpace(_targetRecordName) == true)
            throw new System.NotImplementedException();
        if (_comparisonType == ComparisonType.Null)
            throw new System.NotImplementedException();
    }

    public override bool CheakCondition()
    {
        int data = DataRequest(_targetRecordName);
        if (data == int.MaxValue)
            return false;

        switch (_comparisonType)
        {
            case ComparisonType.More:
                return (data >= _comparisonValue) ? true : false;
            case ComparisonType.Over:
                return (data > _comparisonValue) ? true : false;
            case ComparisonType.Below:
                return (data <= _comparisonValue) ? true : false;
            case ComparisonType.Under:
                return (data < _comparisonValue) ? true : false;
            case ComparisonType.Same:
                return (data == _comparisonValue) ? true : false;
        }
        
        return true;
    }
    
    private int DataRequest(string message)
    {
        return _mediator.GetRecordData(message);
    }
}
