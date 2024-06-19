using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ViewModelBase
{
    public virtual void RefreshViewModel()
    {
        //GameLogicManager.Inst.RefreshCharacterInfo(tempId, vm.OnRefreshViewModel);
    }
    public virtual void RegisterEventsOnEnable()
    {
    }
    public virtual void UnRegisterEventsOnDisable()
    {
    }
    
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
