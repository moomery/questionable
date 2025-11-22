using UnityEngine;
using TMPro;

public class TextRenderer : MonoBehaviour
{
    public TMP_FontAsset f;
    public int FONT_SIZE = 18;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        say("abcde", 18, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Say something at a specific size
    void say(string text, int size, int x, int y)
    {
        TextMeshProUGUI t = gameObject.AddComponent<TextMeshProUGUI>();
        t.text = text;
        t.font = f;
        t.fontSize = size;
    }
}
