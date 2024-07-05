using System;
using System.Collections.Generic;
using System.Reflection;

[Serializable]
public class RecordData : IParserable
{
    public string RecordName;
    public RecordType RecordType;
    public string RecordLimit;
    public float Duration;
    public string RecordResetValue;

    public static void SetParserData<T>(T listInstance, Type genericType, string parserableData)
    {
        object genericInstance = Activator.CreateInstance(genericType);
        var recordUnits = StringParserHelper.CommaParser(parserableData);
        foreach (var recordUnit in recordUnits)
        {
            var keyValuePair = StringParserHelper.FirstColonParser(recordUnit);
            Type fieldType = typeof(RecordData).GetField(keyValuePair.Key).FieldType;
            StringParserHelper.BuiltInTypeParser<RecordData>(genericInstance, fieldType, keyValuePair.Key, keyValuePair.Value);
        }

        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
}