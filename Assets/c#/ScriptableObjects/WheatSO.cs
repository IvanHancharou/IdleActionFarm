using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Wheat")]
public class WheatSO : ScriptableObject
{
    [SerializeField] private int _growTime;
    [Range(1, 10)][SerializeField] private int _cutTimes;
    [SerializeField] private Transform _stackPackPrefab;
    [SerializeField] private Material _sliceMaterial;

    public int CutTimes => _cutTimes;
    public int GrowTime => _growTime;
    public Transform StackPackPrefab => _stackPackPrefab;
    public Material SliceMaterial => _sliceMaterial;
}
