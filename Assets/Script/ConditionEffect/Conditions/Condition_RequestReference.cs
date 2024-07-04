using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_RequestReference : ConditionModule
{
    private string _targetReferenceName;

    public override void InitData(string conditionData)
    {
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
        throw new System.NotImplementedException();
    }
}
