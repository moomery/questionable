using UnityEngine;
using UnityEngine.SceneManagement;

public class Loss : MonoBehaviour
{
    public int lossVal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(lossVal == 0) SceneManager.LoadScene("PhaseB");
            else if(lossVal == 2) SceneManager.LoadScene("PhaseB2");
            else SceneManager.LoadScene("PhaseC");
        }
    }
}
