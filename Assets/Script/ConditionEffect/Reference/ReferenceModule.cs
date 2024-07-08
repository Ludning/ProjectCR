using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceModule
{
    private Mediator _mediator;
    
    private string _referenceName;
    public void InitData(ReferenceData referenceData, Mediator mediator)
    {
        _mediator = mediator;
        _referenceName = referenceData.ReferenceName;
    }
    public int GetValue()
    {
        return PlayerManager.Instance.GetDataByReferenceName(_referenceName);
    }
}
