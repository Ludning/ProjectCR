using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : SingleTonMono<UserDataManager>
{
    public bool IsLogin = false;
    
    public string UserName;
}
