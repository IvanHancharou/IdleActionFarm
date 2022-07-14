using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private AppSettingsSO _app;
    [SerializeField] private Sickle _sickle;
    [SerializeField] private ParticleSystem _mowEffect;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PlayerStackPack _playerStackPack;
    private Animator _animator;
    private Transform _thisTransform;
    private CharacterController _characterController;
    private MowingCollider _mowingCollider;
    private bool _mowInProcess;
    public PlayerStackPack StackPack => _playerStackPack;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _thisTransform = transform;
        _mowingCollider = GetComponentInChildren<MowingCollider>();        
    }

    private void Update()
    {
        float moveZ = _joystick.Vertical;
        float moveX = _joystick.Horizontal;
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        if (moveDirection.magnitude >= 0.1f && !_mowInProcess)
        {
            Move(moveDirection);
        }
        else if (_mowingCollider.CanMowing && !_mowInProcess)
        {
            DoMowing();
        }
        else
        {
            Idle();
        }
    }

    private void Move(Vector3 moveDirection)
    {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        _thisTransform.rotation = Quaternion.Euler(0, targetAngle, 0);
        _characterController.Move(moveDirection * _app.PlayerSpeed * Time.deltaTime);
        _animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);        
    }

    private void DoMowing()
    {
        _animator.SetTrigger("Mow");
        _sickle.Show();
        _mowInProcess = true;
    }

    private void Idle()
    {
        _animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    public void OnMowing()
    {
        _sickle.BoxCollider.enabled = true;
        _mowEffect.Play();
    }

    public void OnMowingDone()
    {
        _sickle.BoxCollider.enabled = false;
    }

    public void OnMowingAnimationDone()
    {
        _mowInProcess = false;
        _sickle.Hide();
    }
}
