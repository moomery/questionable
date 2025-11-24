using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{
    
    float power = 0;
    public GameObject p;
    
    void Update()
    {



        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = mouseWorldPosition - (Vector2)transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg); 

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

        if (Input.GetMouseButton(0))
        {
            power += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ProjectileManager pm = p.GetComponent<ProjectileManager>();

            // Move projectile up by the length of the arrow's sprite.
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            float height = sr.sprite.bounds.size.y;

            float angleRad = angle*Mathf.Deg2Rad;

            Vector2 newPos = new Vector2 (transform.position.x + height*Mathf.Cos(angleRad), transform.position.y + height * Mathf.Sin(angleRad));

            pm.CreateProjectile(newPos, angle, power);
            power = 0;
        }
    }

    
}
    