using UnityEngine;

public class HeartManager : MonoBehaviour
{

    public GameObject heartPrefab;
    public void UpdateHealth(int numHearts)
    {
        for(int i = 0; i < numHearts; i++)
        {
            GameObject obj = Instantiate(heartPrefab, new Vector2(0 + 26*i, 192/2-32), UnityEngine.Quaternion.identity);
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
