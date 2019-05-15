using System;
using UnityEngine;

public class PlayerAttackedEA : EventArgs
{
    public Transform PlayerTransform { get; }
    public Interactable Enemy { get; }
    
    public PlayerAttackedEA(Transform playerTransform, Interactable enemy)
    {
        PlayerTransform = playerTransform;
        Enemy = enemy;
    }
}
