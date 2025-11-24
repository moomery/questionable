using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    public static levelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] UnityEngine.UI.Image _progressBar;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  public async void LoadScene(string sceneName)
{
    _loaderCanvas.SetActive(true);
    _progressBar.fillAmount = 0f;

    await Task.Delay(50); // ensures loader UI has a frame to appear

    var scene = SceneManager.LoadSceneAsync(sceneName);
    scene.allowSceneActivation = false;

    while (scene.progress < 0.9f)
    {
        _progressBar.fillAmount = Mathf.Clamp01(scene.progress / 0.9f);
        await Task.Delay(50);
    }

    _progressBar.fillAmount = 1f;

    // Optional: small delay for visual consistency
    await Task.Delay(200);

    scene.allowSceneActivation = true;
    // Do NOT disable canvas yet — the new scene needs 1 frame to activate
    await Task.Yield();

    _loaderCanvas.SetActive(false);
}

}

