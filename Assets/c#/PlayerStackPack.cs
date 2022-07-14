using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackPack : MonoBehaviour
{
    [SerializeField] private AppSettingsSO _app;
    [SerializeField] private MowingCollider _mowingCollider;
    private Stack<StackPack> _packs = new Stack<StackPack>();
    private Transform _thisTransform;
    public event Action OnSoldPack;
    public event Action<int> OnGetNewPack;
    public int StackLimit => _app.StackLimit;

    private void Start()
    {
        _thisTransform = transform;
        _mowingCollider.OnGetPack += stackPack => GetNextPack(stackPack);
        _mowingCollider.OnBarnFound += barn => StartCoroutine(CargoUnloading(barn));
        UpdateStackInfo();
    }

    private void OnDestroy()
    {
        _mowingCollider.OnGetPack -= stackPack => GetNextPack(stackPack);
        _mowingCollider.OnBarnFound -= barn => StartCoroutine(CargoUnloading(barn));
    }

    private void GetNextPack(StackPack stackPack)
    {
        if (_packs.Count == StackLimit)
        {
            return;
        }

        stackPack.transform.SetParent(_thisTransform);
        if (_packs.Count > 0)
        {
            stackPack.PreviousPack = _packs.Peek().transform;
        }

        stackPack.JumpToStack(new Vector3(0, _packs.Count * 0.1f, 0));
        _packs.Push(stackPack);
        UpdateStackInfo();
    }

    private IEnumerator CargoUnloading(Transform barn)
    {
        while(_packs.Count > 0)
        {
            StackPack st = _packs.Pop();            
            st.JumpToBarn(barn.position);
            yield return new WaitForSeconds(_app.PauseBetweenPackSold);
            OnSoldPack?.Invoke();
        }

        UpdateStackInfo();
    }

    private void UpdateStackInfo()
    {
        OnGetNewPack?.Invoke(_packs.Count);
    }
}
