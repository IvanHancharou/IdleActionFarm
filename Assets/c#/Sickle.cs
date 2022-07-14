using UnityEngine;

public class Sickle : MonoBehaviour
{
    private bool _active = true;
    public bool Active => _active;
    public BoxCollider BoxCollider;

    private void Start()
    {
        BoxCollider = GetComponent<BoxCollider>();
        BoxCollider.enabled = false;
        Hide();
    }

    public void Hide()
    {
        if (_active)
        {
            gameObject.SetActive(false);
            _active = false;
        }
    }

    public void Show()
    {
        if (!_active)
        {
            gameObject.SetActive(true);
            _active = true;
        }
    }
}
