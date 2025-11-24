using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float fac = 500;

    public float angle;
    public float power;
    public Vector2 location;
    public bool first = true; // Prevent the main one from moving
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(first) return;        

        this.location += new Vector2(Time.deltaTime*Mathf.Cos(angle)*fac, Time.deltaTime*Mathf.Sin(angle)*fac);
        this.transform.position = location;

        if(this.location.x > 256/2 || this.location.x < -256/2 || this.location.y > 192/2 || this.location.y < -192/2)
        {
            Destroy(gameObject);
        }
    }

        void OnCollisionEnter2D(Collision2D c)
    {

        if(c.gameObject.name == "fly_0(Clone)")
        {
            Destroy(c.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(c.gameObject.name);
        }
    }

}
