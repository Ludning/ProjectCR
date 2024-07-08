using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int index = 0;
    public ItemType itemType;
    public int archetypeID = 0;
    public int specificityID = 0;
    
    public void InitItemData(string itemData)
    {
        string data = StringParserHelper.ParenthesesParser(itemData);
        List<string> itemPropertys = StringParserHelper.PipeParser(data);
        
        if (itemPropertys.Count > 0 && int.TryParse(itemPropertys[0], out int parsedIndex))
            index = parsedIndex;
        else
            index = 0;
        
        if (itemPropertys.Count > 1) 
            itemType = Enum.TryParse(itemPropertys[1], out ItemType parsedItemType) ? parsedItemType : ItemType.None;

        if (itemPropertys.Count > 2 && int.TryParse(itemPropertys[2], out int parsedArchetypeID))
            archetypeID = parsedArchetypeID;
        else
            archetypeID = 0;
        
        if (itemPropertys.Count > 3 && int.TryParse(itemPropertys[3], out int parsedSpecificityID))
            specificityID = parsedSpecificityID;
        else
            specificityID = 0;
    }
}