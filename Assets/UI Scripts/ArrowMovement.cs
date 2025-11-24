using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{
    
    float power = 0;
    public GameObject p;
    public GameObject h;

    ProjectileManager pm;
    
    float timeElapsed = 0;
    bool hasAddedThisSecond = false;

    public float period = 3;

    void Start()
    {
        HeartManager hm = h.GetComponent<HeartManager>();
        hm.UpdateHealth(3);

        pm = p.GetComponent<ProjectileManager>();
    }

    void Update()
    {

        // Handle other stuff
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
            

            // Move projectile up by the length of the arrow's sprite.
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            float height = sr.sprite.bounds.size.y;

            float angleRad = angle*Mathf.Deg2Rad;

            Vector2 newPos = new Vector2 (transform.position.x + height*Mathf.Cos(angleRad), transform.position.y + height * Mathf.Sin(angleRad));

            pm.CreateProjectile(newPos, angle, power);
            power = 0;
        }



        
        // Create fly every few seconds

        timeElapsed += Time.deltaTime;

        if(Mathf.Round(timeElapsed) % period == 0 && !hasAddedThisSecond)
        {
            hasAddedThisSecond = true;
            float r = Random.Range(0f, 180f);
            float amplitude = 120;

            // Track to the center of sprite
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            float height = sr.sprite.bounds.size.y;
            float angleRad = angle*Mathf.Deg2Rad;
            Vector2 newDest = new Vector2 (transform.position.x + 0.5f*height*Mathf.Cos(angleRad), transform.position.y + 0.5f*height * Mathf.Sin(angleRad));


            pm.CreateFly(new Vector2(Mathf.Cos(r)*amplitude, Mathf.Sin(r)*amplitude), transform.position, 5);
        }
        else if (Mathf.Round(timeElapsed) % period != 0)
        {
            hasAddedThisSecond = false;
        }

    }

    
}
    