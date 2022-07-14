using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/App Settings")]
public class AppSettingsSO : ScriptableObject
{
    [Range(0.1f, 1f)] [SerializeField] private float _pauseBetweenPackSold;
    [Range(0.1f, 1f)] [SerializeField] private float _pauseBetweenCoinAppear;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private int _stackLimit;

    public float PauseBetweenPackSold => _pauseBetweenPackSold;
    public float PauseBetweenCoinAppear => _pauseBetweenCoinAppear;
    public float PlayerSpeed => _playerSpeed;
    public int StackLimit => _stackLimit;
}
