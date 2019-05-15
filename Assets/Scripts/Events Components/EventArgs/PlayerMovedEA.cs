using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovedEA : EventArgs
{
    public Transform PlayerTransform { get; }
    public Transform DestinationTransform { get; }
    
    public PlayerMovedEA(Transform playerTransform, Transform destinationTransform)
    {
        PlayerTransform = playerTransform;
        DestinationTransform = destinationTransform;
    }
}
