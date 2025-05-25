using UnityEngine;

public class Resource2D : MonoBehaviour
{
    [HideInInspector] public bool collected = false;
    [HideInInspector] public DroneAI2D reservedBy = null;

    public void Collect()
    {
        if (collected) return;
        collected = true;
        Destroy(gameObject);
    }
}