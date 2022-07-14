using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPlane : MonoBehaviour
{
    private Vector3 _basePos;
    private Transform _thisTransform;
    public Transform ThisTransform => _thisTransform;

    void Start()
    {
        _thisTransform = transform;
        _basePos = _thisTransform.localPosition;
    }

    public void ChangePos(int damage, int allCutTimes)
    {
        _thisTransform.localPosition = new Vector3(
            _basePos.x,
            _basePos.y,
            _basePos.z - (damage / allCutTimes)
            );
    }
}
