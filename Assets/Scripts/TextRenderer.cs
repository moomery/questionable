using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextRenderer : MonoBehaviour
{
    public TMP_FontAsset f;
    public TextMeshProUGUI DEFAULT_TEXT_OBJECT;
    public int DEFAULT_FONT_SIZE = 18;
    Dictionary<string, GameObject> textObjects = new Dictionary<string, GameObject>();

    public GameObject PARENT_TEST;

    public Canvas CURRENT_CANVAS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DEFAULT_TEXT_OBJECT.fontSize = DEFAULT_FONT_SIZE;
        DEFAULT_TEXT_OBJECT.font = f;

        Say("default text default text default text default text default text default text ");
        InstantiateText("mybox", "Test.", 0, 0, 0);
        InstantiateText("mybox2", "Another test.", 18, 0, 0, 100, 50);

        SayOnObject("squaretext", "HEY I'M ON A SQUARE!!!", 18);

        DeleteText("mybox");
        DeleteText("mybox2");
        UpdateText("squaretext", ".!");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Say(string text)
    {
        DEFAULT_TEXT_OBJECT.text = text;
    }

    void SayOnObject(string id, string text, int size)
    {
        GameObject a = new GameObject(id);
        TextMeshProUGUI t = a.AddComponent<TextMeshProUGUI>();

        a.transform.SetParent(PARENT_TEST.transform, false);

        RectTransform r = a.GetComponent<RectTransform>();
        r.anchoredPosition = new Vector2(r.sizeDelta.x/2, -size); // Account for the size of the bounding box.


        t.text = text;
        t.fontSize = size;
        t.font = f;

        textObjects.Add(id, a);

    }

    // Add text at a specific location on the canvas. (x, y) is the top left corner. (w, h) is the size of the bounding box.
    void InstantiateText(string id, string text, int size, int x, int y, int w, int h)
    {
        GameObject a = new GameObject(id);
        TextMeshProUGUI t = a.AddComponent<TextMeshProUGUI>();

        a.transform.SetParent(CURRENT_CANVAS.transform, false);

        RectTransform r = a.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(w, h);
        r.anchoredPosition = new Vector2(x + r.sizeDelta.x/2, y - r.sizeDelta.y/2); // Account for the size of the bounding box.

        t.text = text;
        t.font = f;
        t.fontSize = size;

        textObjects.Add(id, a);

    }

    // Add text at a specific location on the canvas. (x, y) is the top left corner.
    void InstantiateText(string id, string text, int size, int x, int y)
    {
        GameObject a = new GameObject(id);
        TextMeshProUGUI t = a.AddComponent<TextMeshProUGUI>();

        a.transform.SetParent(CURRENT_CANVAS.transform, false);

        RectTransform r = a.GetComponent<RectTransform>();
        r.anchoredPosition = new Vector2(x + r.sizeDelta.x/2, y - r.sizeDelta.y/2); // Account for the size of the bounding box.

        t.text = text;
        t.font = f;
        t.fontSize = size;

        textObjects.Add(id, a);

    }

    // Add text to a pre-existing game object. TODO: Add offsets.
    void InstantiateText(string id, string text, int size, GameObject parent)
    {
        GameObject a = new GameObject(id);
        TextMeshProUGUI t = a.AddComponent<TextMeshProUGUI>();


        a.transform.SetParent(parent.transform);

        t.text = text;
        t.font = f;
        t.fontSize = size;
        textObjects.Add(id, a);

    }

    void UpdateText(string id, string text)
    {
        TextMeshProUGUI t = textObjects[id].GetComponent<TextMeshProUGUI>();
        t.text = text;
    }

    void DeleteText(string id)
    {
        Destroy(textObjects[id]);
        textObjects.Remove(id);
    }
}
