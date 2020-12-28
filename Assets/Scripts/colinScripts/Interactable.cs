using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocused = false;
    Transform player;

    bool hasInteracted = false;

    // this method is meant to be overridden
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    public void OnFocused (Transform playerTranform)
    {
        isFocused = true;
        player = playerTranform;
        hasInteracted = false;
    }
    public void OnDeFocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }
    // buitin unity function that visualizes our radius
    private void OnDrawGizmosSelected()
    {
        // set own transform as default
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
