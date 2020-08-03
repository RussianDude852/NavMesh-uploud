using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float radius = 5f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {

    }

     void Update()
    {
        if (isFocus)
        {
            if (isFocus && !hasInteracted)
            {


                float distance = Vector3.Distance(player.position, interactionTransform.position);
                if (distance <= radius)
                {
                    Interact();
                    hasInteracted = true;
                }

            }
        }
    }




    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}

