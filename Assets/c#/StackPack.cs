using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StackPack : MonoBehaviour
{
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _jumpPower;
    private Animator _animator;
    private BoxCollider _boxCollider;
    private Transform _thisTransform;
    private bool _isFollowStart;
    public Transform PreviousPack { get; set; }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _thisTransform = transform;
    }

    public void UnchekAnimator()
    {
        _animator.enabled = false;
    }

    public void OnAppear()
    {
        _animator.enabled = false;
        _boxCollider.enabled = true;
    }

    public void JumpToStack(Vector3 targetPos)
    {
        _thisTransform.DOLocalJump(targetPos, _jumpPower, 1, 1).OnComplete(StartFollow);        
    }

    public void JumpToBarn(Vector3 targetPos)
    {
        _thisTransform.parent = null;
        _isFollowStart = false;
        _thisTransform.DOJump(targetPos, _jumpPower, 1, 1);
    }

    public void StartFollow()
    {
        if (PreviousPack == null)
        {
            return;
        }

        _isFollowStart = true;
        _thisTransform.parent = null;
        StartCoroutine(StartFollowingToLastCubePosition(PreviousPack));
    }

    private IEnumerator StartFollowingToLastCubePosition(Transform obj)
    {
        while (_isFollowStart)
        {
            yield return new WaitForEndOfFrame();
            _thisTransform.rotation = obj.rotation;
            Vector3 newPos = new Vector3(obj.position.x, _thisTransform.position.y, obj.position.z);
            _thisTransform.DOMove(newPos, 0.05f);
        }
    }
}
