using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class IndexNameDictionary<T>
{
    private Dictionary<int, string> KeyDictionary;
    private Dictionary<string, T> ValueDictionary;

    public bool TryGetValueByIndex(int index, out T ret)
    {
        if (KeyDictionary.TryGetValue(index, out string name))
        {
            if (ValueDictionary.TryGetValue(name, out T value))
            {
                ret = value;
                return true;
            }
        }
        ret = default(T);
        return false;
    }

    public bool TryGetValueByName(string name, out T ret)
    {
        if (ValueDictionary.TryGetValue(name, out T value))
        {
            ret = value;
            return true;
        }
        ret = default(T);
        return false;
    }

    public IndexNameDictionary(List<T> dataList, FieldInfo indexField, FieldInfo nameField)
    {
        KeyDictionary = new Dictionary<int, string>();
        ValueDictionary = new Dictionary<string, T>();

        foreach (var data in dataList)
        {
            // index 필드의 값을 가져오기
            int indexValue = (int)indexField.GetValue(data);

            // itemName 필드의 값을 가져오기
            string nameValue = (string)nameField.GetValue(data);

            // KeyDictionary에 값 추가
            KeyDictionary[indexValue] = nameValue;

            // ValueDictionary에 값 추가
            ValueDictionary[nameValue] = data;
        }
    }
}
