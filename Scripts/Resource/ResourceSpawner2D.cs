using UnityEngine;

public class ResourceSpawner2D : MonoBehaviour
{
    public GameObject resourcePrefab;
    public float interval = 3f;
    public Vector2 minPos = new Vector2(-8, -4);
    public Vector2 maxPos = new Vector2(8, 4);

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            Vector2 pos = new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));
            Instantiate(resourcePrefab, pos, Quaternion.identity);
        }
    }

    public void SetInterval(float value)
    {
        interval = Mathf.Max(0.5f, value);
    }
}