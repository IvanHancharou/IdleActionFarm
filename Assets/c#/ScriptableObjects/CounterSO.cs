using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Counter")]
public class CounterSO : ScriptableObject
{
    [SerializeField] private Sprite _coinImg;
    [SerializeField] private float _counterSpeed;
    public Sprite CoinImg  => _coinImg;
    public float CounterSpeed => _counterSpeed;
}
