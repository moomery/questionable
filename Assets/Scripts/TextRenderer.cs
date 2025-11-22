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

        SayOnObject(PARENT_TEST, "squaretext", "HEY I'M ON A SQUARE!!!", 18);

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


    public void SayOnObject(GameObject parent, string id, string text, int size)
    {

        if(textObjects.ContainsKey(id))
        {
            UpdateText(id, text);
            return;
        }

        GameObject a = new GameObject(id);
        TextMeshProUGUI t = a.AddComponent<TextMeshProUGUI>();

        a.transform.SetParent(parent.transform, false);


        RectTransform r = a.GetComponent<RectTransform>();
        int selfOffsetX = (int)r.sizeDelta.x/2;
        int selfOffsetY = -size;

        // TODO: Translate w-r-t parent's size.
        SpriteRenderer s = parent.GetComponent<SpriteRenderer>();
        //int parentOffsetX = PARENT_TEST.transform.

        r.anchoredPosition = new Vector2(selfOffsetX, selfOffsetY); // Account for the size of the bounding box.


        t.text = text;
        t.fontSize = size;
        t.font = f;

        textObjects.Add(id, a);

    }

    // Add text at a specific location on the canvas. (x, y) is the top left corner. (w, h) is the size of the bounding box.
    public void InstantiateText(string id, string text, int size, int x, int y, int w, int h)
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
    public void InstantiateText(string id, string text, int size, int x, int y)
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
    public void InstantiateText(string id, string text, int size, GameObject parent)
    {
        GameObject a = new GameObject(id);
        TextMeshProUGUI t = a.AddComponent<TextMeshProUGUI>();


        a.transform.SetParent(parent.transform);

        t.text = text;
        t.font = f;
        t.fontSize = size;
        textObjects.Add(id, a);

    }

    public void UpdateText(string id, string text)
    {
        TextMeshProUGUI t = textObjects[id].GetComponent<TextMeshProUGUI>();
        t.text = text;
    }

    public void DeleteText(string id)
    {
        Destroy(textObjects[id]);
        textObjects.Remove(id);
    }
}
