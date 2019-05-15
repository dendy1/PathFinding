using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOBACamera : MonoBehaviour
{
    [Header("Camera Restrictions")] 
    [SerializeField] private float zoomMinimumFOV;
    [SerializeField] private float zoomMaximumFOV;
    
    [Header("Camera Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float zoomSensetivity;
    [SerializeField] private float borderOffset;
    
    [SerializeField] private bool freeze;

    [SerializeField] private Transform lookAtPivot;

    private Vector3 _defaultPosition;
    private Vector3 _newCameraPosition;
    private Vector3 _offset;

    private Camera _camera;
    private float _zoom;
    
    private Vector3 _desiredPosition;
    
    private void Awake()
    {
        _defaultPosition = transform.position;
        _desiredPosition = transform.position;
        _camera = GetComponent<Camera>();
        _offset = transform.position - lookAtPivot.position;
        _zoom = _camera.fieldOfView;
    }

    void Update()
    {
        if (freeze)
            return;
        
        var mouse = Input.mousePosition;
        
        _zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensetivity;
        _zoom = Mathf.Clamp(_zoom, zoomMinimumFOV, zoomMaximumFOV);
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _zoom, Time.deltaTime * zoomSensetivity);
       
        if (Input.GetKey("w") || mouse.y > Screen.height - borderOffset)
            _desiredPosition += new Vector3(0f, 0f, CameraSpeed);
        
        if (Input.GetKey("a") || mouse.x < borderOffset)
            _desiredPosition += new Vector3(-CameraSpeed, 0f, 0f);
    
        if (Input.GetKey("s") || mouse.y < borderOffset)       
            _desiredPosition += new Vector3(0f, 0f, -CameraSpeed);

        if (Input.GetKey("d") || mouse.x > Screen.width - borderOffset)
            _desiredPosition += new Vector3(CameraSpeed, 0f, 0f);
        
        if (Input.GetKey("space"))
            _desiredPosition = lookAtPivot.position + _offset;
    }

    private void LateUpdate()
    {
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _zoom, Time.deltaTime * zoomSensetivity);
        transform.position = Vector3.Lerp(transform.position, _desiredPosition, CameraSpeed);
    }

    private float CameraSpeed => movementSpeed * Time.deltaTime;
}
