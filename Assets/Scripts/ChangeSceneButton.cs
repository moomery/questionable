using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        levelManager.Instance.LoadScene(sceneName);
    }
}
