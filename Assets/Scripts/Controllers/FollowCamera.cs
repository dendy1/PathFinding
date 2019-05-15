using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Target")] 
    [SerializeField] private Transform target;

    [Header("Distances")] 
    [SerializeField] private float currentDistance;
    [SerializeField] private float minimumDistance;
    [SerializeField] private float maximumDistance;
    [SerializeField] private Vector3 offset;

    [Header("Speeds")] 
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float scrollSensitivity;

    private void LateUpdate()
    {
        if (!target)
        {
            Debug.LogError("No target set for the camera", this);
            return;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * scrollSensitivity;
        currentDistance = Mathf.Clamp(currentDistance, minimumDistance, maximumDistance);

        Vector3 desiredPosition = target.position + offset;
        desiredPosition -= transform.forward * currentDistance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
