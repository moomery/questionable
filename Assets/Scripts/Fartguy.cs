using UnityEngine;

public class Fartguy : MonoBehaviour
{
    Transform t;
    long startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = GetComponent<Transform>();
        startTime = System.DateTime.Now.Ticks;
    }

    // Update is called once per frame
    void Update()
    {
        long x = System.DateTime.Now.Ticks - startTime;

        t.position = new Vector3(90*Mathf.Sin((float)x/12000000), 45*Mathf.Cos((float)x/15000000 + 4), 0);
    }
}
