using UnityEngine;

public class Fly : MonoBehaviour
{


    public GameObject heartManager;
    HeartManager hm;
    public float fac = 50;

    public float angle;
    public float power;
    public Vector2 location;
    public bool first = true; // Prevent the main one from moving
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hm = heartManager.GetComponent<HeartManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(first) return;        

        this.location += new Vector2(Time.deltaTime*Mathf.Cos(angle)*fac, Time.deltaTime*Mathf.Sin(angle)*fac);
        this.transform.position = location;

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.name == "testCanvasSize_0")
        {
            hm.DecrementHealth();
            Destroy(gameObject);
        }
    }
}
