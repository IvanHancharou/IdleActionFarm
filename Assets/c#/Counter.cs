using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private CounterSO _counter;
    [SerializeField] private Image _coinImg;
    private int _sumTotal;
    private int _curSum;

    private void Start()
    {
        _coinImg.sprite = _counter.CoinImg;
    }

    public void UpdateCounter(int sum)
    {
        _sumTotal += sum;
        _coinsText.transform.DOShakePosition(1, 5, 10);
        StartCoroutine(UpdateCounterTextStepByStep());
    }

    public void UpdateSumText()
    {
        _coinsText.text = _curSum.ToString();
    }

    private IEnumerator UpdateCounterTextStepByStep()
    {
        while (_curSum < _sumTotal)
        {
            _curSum++;
            UpdateSumText();
            yield return new WaitForSeconds(_counter.CounterSpeed);
        }
    }
}
