using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class BaseController : MonoBehaviour
{
    protected Interactable CurrentFocus;
    protected NavMeshAgent Agent;
    protected Transform NavMeshTarget;
    
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    
    private void FollowTarget(Interactable newTarget)
    {
        Agent.stoppingDistance = newTarget.InteractionRadius * 0.5f;
        NavMeshTarget = newTarget.transform;
    }

    protected void StopFollowingTarget()
    {
        Agent.stoppingDistance = 0;
        NavMeshTarget = null;
    }
    
    protected void SetFocus(Interactable newFocus)
    {
        if (CurrentFocus != newFocus)
        {
            if (CurrentFocus)
            {
                CurrentFocus.OnDefocused(CurrentFocus.transform);
            }
            
            CurrentFocus = newFocus;
            FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
    }

    protected void RemoveFocus()
    {
        if (CurrentFocus)
        {
            CurrentFocus.OnDefocused(CurrentFocus.transform);
        }

        CurrentFocus = null;
        StopFollowingTarget();
    }
    
    protected void MoveTo(Vector3 point)
    {
        Agent.destination = point;
    }
    
    protected bool RandomPoint(Vector3 center, float radius, out Vector3 result)
    {
        for (int i = 0; i < 30; i++) 
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
