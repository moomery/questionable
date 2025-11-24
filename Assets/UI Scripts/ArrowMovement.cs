using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = mouseWorldPosition - (Vector2)transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90; 

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
    