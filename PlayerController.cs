using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    PlayerMover motor;
    public LayerMask MovementMask;

    public InteractableObject focus;

    // Start is called before the first frame update
    void Start()
    {

        motor = GetComponent<PlayerMover>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        //this is for moving and left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 100, MovementMask))
            {
                //move our player to what we hit
                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }
    }

    void SetFocus (InteractableObject newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        
        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        
        focus = null;
        motor.StopFollowingTarget();
    }

}

