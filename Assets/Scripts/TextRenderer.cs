using UnityEngine;
using TMPro;

public class TextRenderer : MonoBehaviour
{
    public TMP_FontAsset f;
    public TextMeshProUGUI DEFAULT_TEXT_OBJECT;
    public int DEFAULT_FONT_SIZE = 18;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DEFAULT_TEXT_OBJECT.fontSize = DEFAULT_FONT_SIZE;
        DEFAULT_TEXT_OBJECT.font = f;

        say("default text default text default text default text default text default text ");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void say(string text)
    {
        DEFAULT_TEXT_OBJECT.text = text;
    }

    // Say something at a specific size
    void say_custom(string text, int size, int x, int y)
    {
        TextMeshProUGUI t = gameObject.AddComponent<TextMeshProUGUI>();
        t.text = text;
        t.font = f;
        t.fontSize = size;
    }
}
