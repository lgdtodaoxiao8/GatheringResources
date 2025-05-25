using UnityEngine;
using UnityEngine.Events;

public class BaseStation2D : MonoBehaviour
{
    public UnityEvent onResourceDelivered;
    public void Deliver()
    {
        onResourceDelivered?.Invoke();
    }
}