using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DetectTarget : MonoBehaviour
{
    //public DetectionRangeType _detectionRangeType;
    //public float _fieldOfViewAngle = 90;
    
    public LayerMask _targetLayerMask;
    public float _viewDistance = 15;
    [SerializeField] private SphereCollider Collider;
    private List<Transform> _targetList = new List<Transform>();
    
    public int TargetCount => _targetList.Count;
    public Transform Target;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _targetLayerMask) == 0) return;
        
        Debug.Log("타겟이 범위를 진입.");
        _targetList.Add(other.transform);
        if (Target == null)
            Target = other.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & _targetLayerMask) == 0) return;
        
        Debug.Log("타겟이 범위를 벗어남.");
        _targetList.Remove(other.transform);
        if (_targetList.Count == 0)
        {
            Target = null;
        }
        else if (Target == other.transform)
        {
            int index = Random.Range(0, _targetList.Count);
            Target = _targetList[index];
        }
    }

    public void OnDrawGizmos()
    {
#if UNITY_EDITOR
        var oldColor = UnityEditor.Handles.color;
        var color = Color.yellow;
        color.a = 0.1f;
        UnityEditor.Handles.color = color;

        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, Vector3.forward,
            360, _viewDistance);

        UnityEditor.Handles.color = oldColor;
#endif
    }
}
