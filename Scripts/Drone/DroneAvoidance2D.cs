using UnityEngine;

public class DroneAvoidance2D : MonoBehaviour
{
    [SerializeField] float separationDistance = 1f;
    [SerializeField] float strength = 3f;

    void Update()
    {
        Vector2 force = Vector2.zero;
        var hits = Physics2D.OverlapCircleAll(transform.position, separationDistance);
        foreach (var c in hits)
        {
            if (c.gameObject == gameObject || !c.CompareTag("Drone")) continue;
            Vector2 diff = (Vector2)transform.position - (Vector2)c.transform.position;
            force += diff.normalized / Mathf.Max(0.1f, diff.magnitude);
        }
        transform.position += (Vector3)(force * strength * Time.deltaTime);
    }
}