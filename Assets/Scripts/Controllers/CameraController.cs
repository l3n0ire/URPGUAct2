using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    public static CameraController instance;

    // singleton
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public Transform target;
    public Camera firstPersonCamera;
    public Camera mainCamera;

    // short way of creating delegate (pub sub)
    // currentCamera and isfirstPersonEnabled
    public event System.Action<Camera, bool> onCameraChange;

    public Vector3 offset;
    public float firstPersonYawSpeed = 100f;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 500f;

    private float currentYaw = 0f;
    private float currentZoom = 10f;
    private bool isFirstPersonEnabled = false;
    


    void Start()
    {
        mainCamera = GetComponent<Camera>();
        firstPersonCamera.enabled = false;
    }

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        currentYaw += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            isFirstPersonEnabled = !isFirstPersonEnabled;

            if (isFirstPersonEnabled)
            {
                firstPersonCamera.enabled = true;
                mainCamera.enabled = false;
                // invoke event
                if (onCameraChange != null)
                {
                    onCameraChange(firstPersonCamera, isFirstPersonEnabled);
                }
            }
            else
            {
                firstPersonCamera.enabled = false;
                mainCamera.enabled = true;
                // invoke event
                if (onCameraChange != null)
                {
                    onCameraChange(mainCamera, isFirstPersonEnabled);
                }
            }
        }
    }

    void LateUpdate()
    {
        if (isFirstPersonEnabled)
        {
            float rotateSpeed = Input.GetAxis("Mouse X") * firstPersonYawSpeed * Time.deltaTime;
            target.Rotate(Vector3.up * rotateSpeed);

        }
        else
        {
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);
            transform.RotateAround(target.position, Vector3.up, currentYaw);
        }

    }
}
