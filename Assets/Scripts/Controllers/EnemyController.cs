using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : BaseController
{
    [SerializeField] private float visionRadius;
    [SerializeField] private float walkRadius;
    [SerializeField] private float wanderTime;

    private float _timer;

    private void Update()
    {
        Collider[] closeTo = Physics.OverlapSphere(transform.position, visionRadius);

        foreach (var obj in closeTo)
        {
            if (!CurrentFocus && (obj.CompareTag("Player") || obj.CompareTag("Minion")))
            {
                Interactable interactable = obj.GetComponent<Interactable>();
                SetFocus(interactable);
            }
        }

        if (NavMeshTarget)
        {
            MoveTo(NavMeshTarget.position);
        }
        else
        {
            if (_timer <= 0)
            {
                Vector3 point;
                if (RandomPoint(transform.position, walkRadius, out point))
                {
                    MoveTo(point);
                }
                
                _timer = wanderTime;
            }

            _timer -= Time.deltaTime;
        }
    }
}
