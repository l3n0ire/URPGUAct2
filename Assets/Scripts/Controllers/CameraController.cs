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

    public GameObject firstPersonCameraObj;
    public Camera firstPersonCamera;
    public Camera mainCamera;

    // short way of creating delegate (pub sub)
    // currentCamera and isfirstPersonEnabled
    public event System.Action<Camera, bool> onCameraChange;

    private bool isFirstPersonEnabled = false;

    public GameObject camera_target;
    public float camera_move_speed = 80.0f;
    public float clamp_angle = 80.0f;
    public float input_sensitivity = 150.0f;
    public float fp_clamp_angle = 20f;
    public float fp_input_sensitivity = 150.0f;
    float rotation_x = 0.0f;
    float rotation_y = 0.0f;
    float fp_rotation_x = 0.0f;
    float fp_rotation_y = 0.0f;
    float mouse_x;
    float mouse_y;





    void Start()
    {
        mainCamera = transform.GetChild(0).GetComponent<Camera>();
        firstPersonCamera = firstPersonCameraObj.GetComponent<Camera>();
        firstPersonCamera.enabled = false;

        // daniel stuff
        Vector3 rot = transform.localRotation.eulerAngles;
        rotation_x = rot.x;
        rotation_y = rot.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

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
        mouse_x = Input.GetAxis("Mouse X");
        mouse_y = Input.GetAxis("Mouse Y");

        if (isFirstPersonEnabled)
        {

            fp_rotation_x += mouse_x * fp_input_sensitivity * Time.deltaTime;
            fp_rotation_y -= mouse_y * fp_input_sensitivity * Time.deltaTime;
            fp_rotation_y = Mathf.Clamp(fp_rotation_y, -fp_clamp_angle, fp_clamp_angle);
            camera_target.transform.rotation = Quaternion.Euler(fp_rotation_y, fp_rotation_x, 0);
        }
        else
        {
            rotation_x += mouse_x * input_sensitivity * Time.deltaTime;
            rotation_y -= mouse_y * input_sensitivity * Time.deltaTime;
            rotation_y = Mathf.Clamp(rotation_y, -clamp_angle, clamp_angle);
            Quaternion localRotation = Quaternion.Euler(rotation_y, rotation_x, 0);
            transform.rotation = localRotation;
            CameraUpdater();
        }

    }
    void CameraUpdater()
    {
        Transform target = camera_target.transform;
        float step = camera_move_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
