using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    private bool _isFocus;
    private List<Transform> _focusedTransforms;

    bool hasInteracted;

    private void Awake()
    {
        _focusedTransforms = new List<Transform>();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactionTransform, this);
    }

    private void Update()
    {
        if (_isFocus && !hasInteracted)
        {
            foreach (var focused in _focusedTransforms)
            {
                 float distance = Vector3.Distance(focused.position, transform.position);
                
                if (distance <= radius)
                {
                    Interact();
                    hasInteracted = true;
                }
            }
        }
    }

    public void OnFocused(Transform focusTransform)
    {
        _isFocus = true;
        if (!_focusedTransforms.Contains(focusTransform))
            _focusedTransforms.Add(focusTransform);
        hasInteracted = false;
    }

    public void OnDefocused(Transform focusTransform)
    {
        _isFocus = false;
        if (_focusedTransforms.Contains(focusTransform))
            _focusedTransforms.Remove(focusTransform);
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
