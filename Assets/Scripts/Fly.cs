using UnityEngine;

public class Fly : MonoBehaviour
{

    public float fac = 50;

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

    }
}
