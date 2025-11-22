using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TempSelectorBody;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          //selector positions
    float SelectorX = 0.0f;
    float SelectorZ = 0.0f;
    float SelectorY = -5.0f;
    Vector3 spawnPosition = new Vector3 (SelectorX, SelectorY, SelectorZ);

     Instantiate(TempSelectorBody, spawnPosition, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
