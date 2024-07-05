using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_RandomChance : ConditionModule
{
    private int? _maxRange = null;
    private int? _chancePercent = null;
    public override void InitData(string conditionData, Mediator mediator)
    {
        string data = StringParserHelper.ParenthesesParser(conditionData);
        List<string> dataStrings = StringParserHelper.PipeParser(data);
        foreach (var dataString in dataStrings)
        {
            var keyValueData = StringParserHelper.FirstColonParser(dataString);
            switch (keyValueData.Key)
            {
                case "MaxRange":
                    _maxRange = int.Parse(keyValueData.Value);
                    break;
                case "ChancePercent":
                    _chancePercent = int.Parse(keyValueData.Value);
                    break;
            }
        }

        if (_maxRange.HasValue == false)
            _maxRange = 100;
        if (_chancePercent.HasValue == false)
            _chancePercent = 50;
    }

    public override bool CheakCondition()
    {
        int value = Random.Range(0, _maxRange.Value);
        return (value < _chancePercent) ? true : false;
    }
}
