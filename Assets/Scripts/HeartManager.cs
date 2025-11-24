using UnityEngine;
using System.Collections.Generic;
public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;
    int numHearts = 3;
    public List<GameObject> hearts;
    public GameObject c;

    TextRenderer t;
    public void DecrementHealth()
    {

        if(hearts.Count == 0)
        {
            t.Say("YOU LOSE!"); 
            return;
        }

        Debug.Log(hearts.Count);
        hearts[hearts.Count - 1].SetActive(false);
        Destroy(hearts[hearts.Count - 1]);
        hearts.RemoveAt(hearts.Count - 1);


    }

    public void UpdateHealth(int numHearts)
    {
        this.numHearts = numHearts;
        for(int i = 0; i < numHearts; i++)
        {
            GameObject obj = Instantiate(heartPrefab, new Vector2(0 + 26*i, 192/2-32), UnityEngine.Quaternion.identity);
            hearts.Add(obj);
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = c.GetComponent<TextRenderer>();
        hearts = new List<GameObject>();
        UpdateHealth(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
