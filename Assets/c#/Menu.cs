using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text _stackText;
    [SerializeField] private Counter _counter;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _bar;
    [SerializeField] private Player _player;
    [SerializeField] private CoinSO _coin;
    [SerializeField] private AppSettingsSO _app;

    private void Start()
    {
        _player.StackPack.OnSoldPack += () => StartCoroutine(WaitCoins());
        _player.StackPack.OnGetNewPack += (count) => SetStackText(count);
    }

    private void OnDestroy()
    {
        _player.StackPack.OnSoldPack -= () => StartCoroutine(WaitCoins());
        _player.StackPack.OnGetNewPack -= (count) => SetStackText(count);
    }

    public void SetStackText(int count)
    {
        _stackText.text = $"{count}/{_app.StackLimit}";
    }

    public void SetCounter(GameObject coin)
    {
        Destroy(coin);
        _counter.UpdateCounter(_coin.Cost);
    }

    private IEnumerator WaitCoins()
    {
        yield return new WaitForSeconds(_app.PauseBetweenCoinAppear);
        var coin = new GameObject("Coin");
        coin.transform.SetParent(transform);
        coin.AddComponent<Image>().sprite = _coin.Icon;
        coin.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
        coin.GetComponent<RectTransform>().anchoredPosition = GetCoinStartPos();

        coin.transform.DOMove(_counter.transform.position, 1).OnComplete(() => SetCounter(coin));
    }

    private Vector2 GetCoinStartPos()
    {
        Vector2 viewPos = _camera.WorldToViewportPoint(_bar.position);
        return new Vector2(
            (viewPos.x * _canvas.sizeDelta.x) - (_canvas.sizeDelta.x * 0.5f),
            (viewPos.y * _canvas.sizeDelta.y) - (_canvas.sizeDelta.y * 0.5f));
    }
}
