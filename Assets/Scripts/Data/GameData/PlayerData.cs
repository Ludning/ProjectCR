using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Sirenix.OdinInspector;

[Serializable]
public class PlayerData : IParserable
{
    public string identification;
    public string password;
    public string nickname;
    public string character;
    public int level;
    public int exp;
    
    
    //public string ownedSkill_data;
    //public string equipmentSkill_data;
    
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T dataInstance) where T : IParserable
    {
        foreach (KeyValuePair<string, int> keyValuePair in columnTypeDic)
        {
            FieldInfo fieldInfo = typeof(T).GetField(keyValuePair.Key, BindingFlags.Public | BindingFlags.Instance);
            Type fieldType = fieldInfo.FieldType;

            if (dataRow[keyValuePair.Value] == DBNull.Value)
                continue;

            object listInstance;
            Type genericType;
            
            switch (keyValuePair.Key)
            {
                case "inventory_data":
                    listInstance = fieldInfo.GetValue(dataInstance);
                    genericType = fieldType.GetGenericArguments()[0];
                    if (listInstance == null)
                    {
                        listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                        fieldInfo.SetValue(dataInstance, listInstance);
                    }

                    string inventoryCellData = dataRow[keyValuePair.Value].ToString();
                    var inventoryDatas = StringParserHelper.BracesParser(inventoryCellData);
                    foreach (var inventoryData in inventoryDatas)
                    {
                        Type listType = typeof(List<>).MakeGenericType(genericType);
                        MethodInfo addMethod = listType.GetMethod("Add");
                        addMethod.Invoke(listInstance, new object[] { inventoryData });
                    }
                    break;
                case "equipment_data":
                    listInstance = fieldInfo.GetValue(dataInstance);
                    genericType = fieldType.GetGenericArguments()[0];
                    if (listInstance == null)
                    {
                        listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                        fieldInfo.SetValue(dataInstance, listInstance);
                    }

                    string equipmentCellData = dataRow[keyValuePair.Value].ToString();
                    var equipmentDatas = StringParserHelper.BracesParser(equipmentCellData);
                    foreach (var equipmentData in equipmentDatas)
                    {
                        Type listType = typeof(List<>).MakeGenericType(genericType);
                        MethodInfo addMethod = listType.GetMethod("Add");
                        addMethod.Invoke(listInstance, new object[] { equipmentData });
                    }
                    break;
                default:
                    StringParserHelper.BuiltInTypeParser<T>(dataInstance, fieldType, fieldInfo.Name, dataRow[keyValuePair.Value]);
                    break;
            }
            

            
        }
    }
}