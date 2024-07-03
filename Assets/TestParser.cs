using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string text = "{Trigger:KillMonster, SetRecordValue:(RecordName:Predation|Value:1)}\n{Trigger:KillElite, SetRecordValue(RecordName:Predation|Value:2), SetRecordDuration:(RecordName:Predation|Duration:10)}\n{Trigger:KillBoss, SetRecordValue(RecordName:Predation|Value:3), SetRecordDuration:(RecordName:Predation|Duration:15)}\n{RequestRecordValue:(RecordName:Predation|Comparison:Same|Value:1), IncreasedStat:(Type:Damage|Value:10)}\n{RequestRecordValue:(RecordName:Predation|Comparison:Same|Value:2), IncreasedStat:(Type:Damage|Value:15)}\n{RequestRecordValue:(RecordName:Predation|Comparison:Same|Value:3), IncreasedStat:(Type:Damage|Value:20)}";
        var conditionEffects = StringParserHelper.BracesParser(text);
        foreach (var conditionEffect in conditionEffects)
        {
            var effectUnits = StringParserHelper.CommaParser(conditionEffect);
            foreach (var effectUnit in effectUnits)
            {
                var conditionAndEffects = StringParserHelper.FirstColonParser(effectUnit);
                Debug.Log(conditionAndEffects.Key);
                var effects = StringParserHelper.ParenthesesParser(conditionAndEffects.Value);
                Debug.Log(effects);
            }
        }
    }
}
