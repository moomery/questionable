using UnityEngine;

public class Fartguy : MonoBehaviour
{


    float timeElapsed = 0;
    public GameObject pmObj;
    ProjectileManager p;
    float period = 3;
    bool hasAddedThisSecond = false;

    Transform t;
    long startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = GetComponent<Transform>();
        startTime = System.DateTime.Now.Ticks;

        p = pmObj.GetComponent<ProjectileManager>();
    }


    void SpawnFly()
    {

        // Track to the center of sprite
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        float height = sr.sprite.bounds.size.y;
        Vector2 newDest = new Vector2 (transform.position.x, transform.position.y + 0.5f*height);


        p.CreateFly(new Vector2(transform.position.x, transform.position.y - 35), new Vector2(0, 0), 5);

    }

    // Update is called once per frame
    void Update()
    {
        long x = System.DateTime.Now.Ticks - startTime;
        timeElapsed += Time.deltaTime;


        t.position = new Vector3(90*Mathf.Sin((float)x/12000000), 80 + 23*Mathf.Cos((float)x/5000000 + 4), 0);
        // Spawn fly
        if(Mathf.Round(timeElapsed*6) % period == 0 && !hasAddedThisSecond)
        {
            hasAddedThisSecond = true;
            SpawnFly();
        }
        else if (Mathf.Round(timeElapsed * 6) % period != 0)
        {
            hasAddedThisSecond = false;
        }
    }
}
