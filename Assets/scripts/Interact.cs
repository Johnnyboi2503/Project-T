using UnityEngine;

public class Interact : MonoBehaviour
{
    public Collider2D InteractCollider2D;
    public Interactable InteractableOBJscript;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableOBJscript = collision.GetComponent<Interactable>();
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        InteractableOBJscript = null;
    }
    public void interact()
    {
        if(InteractableOBJscript != null)
        {
            InteractableOBJscript.Interacted();//dont chande the name of the function in the interactable scripts
        }
    }
}
