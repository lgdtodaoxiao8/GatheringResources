using UnityEngine;
using System.Collections;
using System.Linq;

public class DroneAI2D : MonoBehaviour
{
    public enum State { Seeking, Collecting, Returning }
    public float speed = 5f;
    public float collectTime = 2f;
    public bool drawPath;
    LineRenderer lr;
    Resource2D target;
    public BaseStation2D home;
    State state;
    Vector2 destination;
    Rigidbody2D rb;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(BaseStation2D baseStation, float spd, bool draw)
    {
        home = baseStation;
        speed = spd;
        drawPath = draw;
        state = State.Seeking;
        NextTarget();
    }

    void Update()
    {
        if (rb == null || home == null)
            return;
        switch (state)
        {
            case State.Seeking:
                if (target == null || target.collected)
                {
                    NextTarget();
                    return;
                }
                MoveTo(target.transform.position);
                if (Vector2.Distance(transform.position, target.transform.position) < 0.15f)
                    StartCoroutine(CollectRoutine());
                break;
            case State.Returning:
                MoveTo(home.transform.position);
                if (Vector2.Distance(transform.position, home.transform.position) < 0.2f)
                {
                    home.Deliver();
                    StartCoroutine(DeliverEffect());
                    state = State.Seeking;
                    NextTarget();
                }
                break;
        }
        if (drawPath && lr != null)
        {
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, destination);
        }
        else if (lr != null)
        {
            lr.positionCount = 0;
        }
    }

    void MoveTo(Vector2 pos)
    {
        destination = pos;
        Vector2 dir = (pos - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    IEnumerator CollectRoutine()
    {
        state = State.Collecting;
        yield return new WaitForSeconds(collectTime);
        if (target != null) {
            target.Collect();
            target.reservedBy = null;
        }
        state = State.Returning;
        destination = home.transform.position;
    }

    IEnumerator DeliverEffect()
    {
        Vector3 orig = transform.localScale;
        transform.localScale = orig * 1.2f;
        yield return new WaitForSeconds(0.2f);
        transform.localScale = orig;
    }

    void NextTarget()
    {
        if (target != null && target.reservedBy == this)
            target.reservedBy = null;
        var resources = FindObjectsOfType<Resource2D>()
            .Where(r => !r.collected && (r.reservedBy == null || r.reservedBy == this))
            .OrderBy(r => Vector2.Distance(transform.position, r.transform.position))
            .ToArray();
        if (resources.Length == 0)
        {
            target = null;
            return;
        }
        target = resources[0];
        target.reservedBy = this;
        destination = target.transform.position;
    }
}