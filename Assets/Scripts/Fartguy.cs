using UnityEngine;
using UnityEngine.SceneManagement;

public class Fartguy : MonoBehaviour
{

    bool hasAddedHorde = false;
    public GameObject healthBar;
    public GameObject PSA;
    TextRenderer psa;
    TextRenderer hb;
    string bossFullHealthString = "BOSS HEALTH: XXXXXXXXXXXXXXXXXXXXX";

    float bossMaxHealth = 40f;
    float bossCurrentHealth = 40f;

    Vector2 target = new Vector2(0, 0);
    float timeElapsed = 0;
    public GameObject pmObj;
    ProjectileManager p;
    float period = 3;
    bool hasAddedThisSecond = false;

    float phaseDuration = 9;

    Transform t;
    long startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = GetComponent<Transform>();
        startTime = System.DateTime.Now.Ticks;

        p = pmObj.GetComponent<ProjectileManager>();
        hb = healthBar.GetComponent<TextRenderer>();
        psa = PSA.GetComponent<TextRenderer>();
    }

    public void DecrementHealth()
    {
        bossCurrentHealth -= 1;

        if(bossCurrentHealth == 0)
        {
            psa.Say("You Win!");
            Destroy(gameObject);
            SceneManager.LoadScene("Win Scene");
        }

        string bar = "";
        for(int i = 0; i < bossFullHealthString.Length *  (bossCurrentHealth/bossMaxHealth); i++)
        {
            bar = bar + bossFullHealthString[i];
        }

        hb.Say(bar);
    }
    public void SetTarget(Vector2 t)
    {
        this.target = t;
    }
    void SpawnFly()
    {

        // Track to the center of sprite
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        float height = sr.sprite.bounds.size.y;


        p.CreateFly(new Vector2(transform.position.x, transform.position.y - 35), this.target, 5);

    }

    void LegacySpawnFly()
    {
        float r = Random.Range(0f, 3.14f);
        float amplitude = 120;

        // Track to the center of sprite
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        float height = sr.sprite.bounds.size.y;
        Vector2 newDest = new Vector2 (transform.position.x, transform.position.y + 0.5f*height);


        p.CreateFly(new Vector2(Mathf.Cos(r)*amplitude, Mathf.Sin(r)*amplitude), this.target, 5);

    }

    void Burst()
    {
        // BURST!!!
        if(Mathf.Round(timeElapsed*12) % period == 0 && !hasAddedThisSecond)
        {
            hasAddedThisSecond = true;
            SpawnFly();
        }
        else if (Mathf.Round(timeElapsed * 12) % period != 0)
        {
            hasAddedThisSecond = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        long x = System.DateTime.Now.Ticks - startTime;
        timeElapsed += Time.deltaTime;

        if(timeElapsed % phaseDuration < 6)
        {
            hasAddedHorde = false;
            Burst();        
        }
        else if(!hasAddedHorde)
        {
            hasAddedHorde = true;
            LegacySpawnFly();
            LegacySpawnFly();
            LegacySpawnFly();
            
        }

        t.position = new Vector3(90*Mathf.Sin((float)x/12000000), 80 + 23*Mathf.Cos((float)x/5000000 + 4), 0);

    }
}
