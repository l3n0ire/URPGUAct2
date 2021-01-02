using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    public GameObject camera_target;
    public float camera_move_speed = 80.0f;
    public float clamp_angle = 80.0f;
    public float input_sensitivity = 150.0f;
    float rotation_x = 0.0f;
    float rotation_y = 0.0f;  
    float mouse_x;
    float mouse_y;
    

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotation_x = rot.x;
        rotation_y = rot.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        mouse_x = Input.GetAxis("Mouse X");
        mouse_y = Input.GetAxis("Mouse Y");
        rotation_x += mouse_x * input_sensitivity * Time.deltaTime;
        rotation_y -= mouse_y * input_sensitivity * Time.deltaTime;
        rotation_y = Mathf.Clamp(rotation_y, -clamp_angle, clamp_angle);
        Quaternion localRotation = Quaternion.Euler(rotation_y, rotation_x, 0); 
        transform.rotation = localRotation;
    }

    void LateUpdate() 
    {
        CameraUpdater();    
    }

    void CameraUpdater()
    {
        Transform target = camera_target.transform;
        float step = camera_move_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

}
