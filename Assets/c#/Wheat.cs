using System.Collections;
using UnityEngine;
using EzySlice;

public class Wheat : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private CutPlane _cutPlane;
    [SerializeField] private WheatSO _wheatSO;    
    private Transform _thisTransform;
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;
    public bool Grow { get; private set; }
    private GameObject[] _hulls = new GameObject[2];

    private void Start()
    {
        _thisTransform = transform;
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sickle>() && !Grow)
        {
            GetDamage();
        }
    }

    public void GetDamage()
    {
        ClearCuttingParts();
        _damage++;
        _meshRenderer.enabled = false;
        _cutPlane.ChangePos(_damage, _wheatSO.CutTimes);
        _hulls = Slice(_cutPlane.ThisTransform.position, _cutPlane.ThisTransform.transform.up);
        if (_hulls != null)
        {
            foreach (GameObject obj in _hulls)
            {
                obj.transform.SetParent(_thisTransform.parent);
                obj.transform.position = _thisTransform.position;
            }

            _hulls[0].AddComponent<Rigidbody>();
        }

        if (_damage == _wheatSO.CutTimes)
        {
            StartCoroutine(WaitDestroyCuttingParts());            
        }             
    }

    private void Update()
    {
        if (Grow)
        {
            Growing();
        }
    }

    private void Growing()
    {
        _thisTransform.localScale = new Vector3(
            _thisTransform.localScale.x + Time.deltaTime / _wheatSO.GrowTime,
            _thisTransform.localScale.y + Time.deltaTime / _wheatSO.GrowTime,
            _thisTransform.localScale.z + Time.deltaTime / _wheatSO.GrowTime);
        if (_thisTransform.localScale.z >= 1f)
        {
            Grow = false;
            _boxCollider.enabled = true;
        }
    }

    public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection)
    {
        return gameObject.SliceInstantiate(planeWorldPosition, planeWorldDirection, _wheatSO.SliceMaterial);
    }

    private IEnumerator WaitDestroyCuttingParts()
    {
        _thisTransform.localScale = Vector3.zero;
        _damage = 0;
        Grow = true;
        Instantiate(_wheatSO.StackPackPrefab, _thisTransform.position, Quaternion.identity, _thisTransform.parent);
        yield return new WaitForSeconds(0.5f);
        _boxCollider.enabled = false;
        _meshRenderer.enabled = true;
        ClearCuttingParts();
    }

    public void ClearCuttingParts()
    {
        foreach (GameObject obj in _hulls)
        {
            Destroy(obj);
        }
    }
}
