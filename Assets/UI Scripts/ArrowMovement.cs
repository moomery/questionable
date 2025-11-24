using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ArrowMovement : MonoBehaviour
{

    public string phase;
    public GameObject gjwtr;
    TextRenderer tr;
    
    float power = 0;
    public GameObject p;
    public GameObject h;

    ProjectileManager pm;
    
    float timeElapsed = 0;
    bool hasAddedThisSecond = false;

    public float period = 3;

    float difficultyMultiplier = 1;

    void Start()
    {
        tr = gjwtr.GetComponent<TextRenderer>();
        pm = p.GetComponent<ProjectileManager>();
    }


    void SpawnFly()
    {
        float r = Random.Range(0f, 3.14f);
        float amplitude = 120;

        // Track to the center of sprite
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        float height = sr.sprite.bounds.size.y;
        Vector2 newDest = new Vector2 (transform.position.x, transform.position.y + 0.5f*height);


        pm.CreateFly(new Vector2(Mathf.Cos(r)*amplitude, Mathf.Sin(r)*amplitude), transform.position, 5);

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

        if(phase == "B1") 
        {
            timeElapsed += Time.deltaTime;
        }
        else if (phase == "B2") 
        {
            timeElapsed += Time.deltaTime*1.1f;
        }

        if(Mathf.Round(timeElapsed) % period == 0 && !hasAddedThisSecond)
        {

            if(phase == "B1")
            {
                if(timeElapsed < 4)
                {            
                    SpawnFly();
                }
                else if (timeElapsed < 10)
                {
                    SpawnFly();
                    SpawnFly();
                }
                else if(timeElapsed < 16)
                {
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                }
                else if(timeElapsed < 24)
                {
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();

                } else if (timeElapsed < 40)
                {
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                } else
                {
                    tr.Say("You win!");
                    SceneManager.LoadScene("PhaseA2");
                }
            }
            else if(phase == "B2")
            {
                if(timeElapsed < 4)
                {            
                    SpawnFly();
                    SpawnFly();
                }
                else if (timeElapsed < 8)
                {
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                }
                else if(timeElapsed < 20)
                {
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                }
                 else if (timeElapsed < 30)
                {
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                    SpawnFly();
                } else
                {
                    tr.Say("You win!");
                    SceneManager.LoadScene("PhaseA3");
                }
            }


            hasAddedThisSecond = true;
        }
        else if (Mathf.Round(timeElapsed) % period != 0)
        {
            hasAddedThisSecond = false;
        }

    }

    
}
    