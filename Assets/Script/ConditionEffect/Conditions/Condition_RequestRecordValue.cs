using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_RequestRecordValue : ConditionModule
{
    private string _targetRecordName;
    private ComparisonType _comparisonType;
    private float? _comparisonValue;
    public override void InitData(string conditionData)
    {
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
                    _comparisonValue = float.Parse(keyValueData.Value);
                    break;
            }
        }

        if (string.IsNullOrWhiteSpace(_targetRecordName) == true)
            throw new System.NotImplementedException();
        if (_comparisonType == ComparisonType.Null)
            throw new System.NotImplementedException();
        if (_comparisonValue.HasValue == false)
            throw new System.NotImplementedException();
            
    }

    public override bool CheakCondition()
    {
        
    }
}
