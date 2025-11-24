using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float fac = 500;

    public float angle;
    public float power;
    public Vector2 location;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.location += new Vector2(Time.deltaTime*Mathf.Cos(angle)*fac, Time.deltaTime*Mathf.Sin(angle)*fac);
        this.transform.position = location;
    }
}
