using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public Camera cam;
    public LayerMask npcLayer;
    public UIManager uiManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, npcLayer))
        {
            NPCInteraction npc = hit.collider.GetComponent<NPCInteraction>();

            if (npc != null)
            {
                npc.Interact(transform);
            }
        }
    }
}