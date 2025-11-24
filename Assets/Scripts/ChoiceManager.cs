using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceManager : MonoBehaviour
{
    private GameObject selectedObject;

    public DialogueManager dialogueManager; // reference to your DialogueManager

    void Update()
    {
        // Only process clicks when a choice is active
        if (!dialogueManager.waitingForChoice) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Get mouse position in world space
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // Raycast to detect clicked objects
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clicked = hit.collider.gameObject;

                // Check if this object is one of the dialogue choices
                if (dialogueManager.textRenderer.textObjects.ContainsValue(clicked))
                {
                    // Use the name of the object as the choice string
                    string choice = clicked.name;

                    // Tell DialogueManager to handle the choice
                    dialogueManager.OnChoiceSelected(choice);
                }
            }
        }
    }
}
