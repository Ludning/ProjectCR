using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_RandomChance : ConditionModule
{
    private int _maxRange = int.MaxValue;
    private int _chancePercent = int.MaxValue;
    public override void InitData(string conditionData)
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

        if (_maxRange == int.MaxValue)
            _maxRange = 100;
        if (_chancePercent == int.MaxValue)
            _chancePercent = 50;
    }

    public override bool CheakCondition()
    {
        int value = Random.Range(0, _maxRange);
        return (value < _chancePercent) ? true : false;
    }
}
