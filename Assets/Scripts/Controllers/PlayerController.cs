using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    Camera cam;

    PlayerMotor motor;
    public Interactable focus;

    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private bool isFirstPersonEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        CameraController.instance.onCameraChange += OnCameraChange;
        motor = GetComponent<PlayerMotor>();


    }

    void OnCameraChange(Camera currentCamera, bool isFirstPersonEnabled)
    {
        cam = currentCamera;
        this.isFirstPersonEnabled = isFirstPersonEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        // prevent movemnt/interaction when inventory ui is active
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        /* click on ground to move
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
                // move our player to what we hit

                RemoveFocus();
            }
        }*/

        // wasd input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // remove focus when player moves
            RemoveFocus();

            
            // account for camera rotation
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            direction = Quaternion.Euler(0f, angle, 0f)*Vector3.forward;

            // enable rotation if not ranged mode
            if(!isFirstPersonEnabled)
            {
                // player rotation for third person only
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            Vector3 moveDestination = transform.position + direction;
            motor.MoveToPoint(moveDestination);

        }



        // left click to interact
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // focus on interactable objects
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    SetFocus(interactable);
                }

            }
            
        }

    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus){
            if(focus!=null)
                focus.OnDeFocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if(focus != null)
            focus.OnDeFocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
