using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Coin")]
public class CoinSO : ScriptableObject
{
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _icon;
    public int Cost => _cost;
    public Sprite Icon => _icon;
}
