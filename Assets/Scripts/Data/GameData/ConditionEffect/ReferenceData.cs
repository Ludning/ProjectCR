using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ReferenceData : IParserable
{
    public string ReferenceName;
    //public RecordType ReferenceType;

    public static void SetParserData<T>(T listInstance, Type genericType, string parserableData)
    {
        object genericInstance = Activator.CreateInstance(genericType);
        var recordUnits = StringParserHelper.CommaParser(parserableData);
        foreach (var recordUnit in recordUnits)
        {
            var keyValuePair = StringParserHelper.FirstColonParser(recordUnit);
            Type fieldType = typeof(ReferenceData).GetField(keyValuePair.Key).FieldType;
            StringParserHelper.BuiltInTypeParser<ReferenceData>(genericInstance, fieldType, keyValuePair.Key, keyValuePair.Value);
        }

        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
}
