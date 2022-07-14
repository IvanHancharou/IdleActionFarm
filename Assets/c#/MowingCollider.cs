using UnityEngine;
using System;

public class MowingCollider : MonoBehaviour
{
    public bool CanMowing { get; private set; }
    public event Action<StackPack> OnGetPack;
    public event Action<Transform> OnBarnFound;

    private void OnTriggerEnter(Collider other)
    {        
        // не срезаем во время роста
        if (other.TryGetComponent(out Wheat wheat))
        {
            if (wheat.Grow)
            {
                return;
            }

            CanMowing = true;
        }
        else if (other.TryGetComponent(out StackPack stackPack))
        {
            OnGetPack?.Invoke(stackPack);
        }
        else if (other.CompareTag("Barn Collider"))
        {
            OnBarnFound?.Invoke(other.transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Wheat>())
        {
            CanMowing = false;
        }
    }
}
