using System;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    ATTACKING,
    FOLLOWING,
    BERSERK
}

public class MinionController : BaseController
{
    [SerializeField] private float visionRadius;

    private State _currentState;
    private float _defaultVision;
    private float _defaultSpeed;

    private Collider[] _enemies;
    
    public Vector3 CheckPoint { get; set; }
    
    public State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;

            if (value == State.BERSERK)
            {
                visionRadius = 500f;
                Agent.speed *= 3f;
                var newTarget = FindClosestEnemy();
                SetFocus(newTarget.GetComponent<Interactable>());
            }
            else if (value == State.ATTACKING)
            {
                StopFollowingTarget();
                RemoveFocus();
                visionRadius = _defaultVision;
                Agent.speed = _defaultSpeed;
            }
            else
            {
                CheckPoint = Vector3.negativeInfinity;
                visionRadius = _defaultVision;
                Agent.speed = _defaultSpeed;
                SetFocus(PlayerManager.Instance.Player.GetComponent<Interactable>());
            }
        }
    }
    
    private void Start()
    {
        EventManager.SubscribeToEvent("PlayerAttacked", OnPlayerAttacked);

        _defaultSpeed = Agent.speed;
        _defaultVision = visionRadius;
        _enemies = new Collider[50];

        CurrentState = State.FOLLOWING;
    }

    private void Update()
    {
        if (CurrentState == State.FOLLOWING)
        {
            if (Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position) > visionRadius)
            {
                SetFocus(PlayerManager.Instance.Player.GetComponent<Interactable>());
            }
            else
            {
                Collider newTarget = FindClosestEnemy();

                if (newTarget)
                {
                    SetFocus(newTarget.GetComponent<Interactable>());
                }
                else
                {
                    SetFocus(PlayerManager.Instance.Player.GetComponent<Interactable>());
                }
            }
            
        }
        else if (CurrentState == State.BERSERK)
        {
            if (!CurrentFocus)
            {
                Collider newTarget = FindClosestEnemy();
                
                if (newTarget)
                {
                    SetFocus(newTarget.GetComponent<Interactable>());
                }
            }
        }
        else
        {
            if (CheckPoint == Vector3.negativeInfinity || Vector3.Distance(transform.position, CheckPoint) <= visionRadius)
            {
                CurrentState = State.FOLLOWING;
            }
            else
            {
                MoveTo(CheckPoint);
                
                Collider newTarget = FindClosestEnemy();
                
                if (newTarget)
                {
                    SetFocus(newTarget.GetComponent<Interactable>());
                }
            }
        }
        
        if (NavMeshTarget)
        {
            SetFocus(CurrentFocus);
            MoveTo(NavMeshTarget.position);
        }
    }
    
    private void OnPlayerAttacked(object sender, EventArgs args)
    {
        if (CurrentState == State.FOLLOWING)
            SetFocus(((PlayerAttackedEA)args).Enemy);
    }

    private Collider FindClosestEnemy()
    {
        var enemiesCount = Physics.OverlapSphereNonAlloc(transform.position, visionRadius, _enemies);

        float minDistance = Mathf.Infinity;
        Collider tempTarget = null;
        
        for (int i = 0; i < enemiesCount; i++)
        {
            if (!_enemies[i].CompareTag("Enemy"))
                continue;
            
            float currentDistance = Vector3.Distance(transform.position, _enemies[i].transform.position);
            if (currentDistance < minDistance)
            {
                tempTarget = _enemies[i];
                minDistance = currentDistance;
            }
        }

        return tempTarget;
    }

    public void MoveToPoint(Vector3 point)
    {
        MoveTo(point);
    }
}