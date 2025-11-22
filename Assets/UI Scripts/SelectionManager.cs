
using UnityEngine;
using UnityEngine.InputSystem;


public class SelectionManager : MonoBehaviour
{   private GameObject selectedObject;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            UnityEngine.Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            //get mouse position global
            UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            //raycast
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (selectedObject != null && selectedObject != hit.collider.gameObject)
                {
                    Debug.Log ("Deselected: " + selectedObject.name);
                }
                //select new object
                if(selectedObject != null)
                {
                    // "Un-select" current object
                    selectedObject.GetComponent<SpriteRenderer>().color = Color.white;
                }

                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log("Selected: " + selectedObject.name);
            }
            else
            {
                if (selectedObject != null)
                {
                    Debug.Log("Deselected: " + selectedObject.name);
                    selectedObject.GetComponent<SpriteRenderer>().color = Color.white;
                    selectedObject = null;
                }
            }

        }
   
    }
}
