using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Numerics;
using TMPro;


public class DialogueManager: MonoBehaviour
{
    public TextRenderer textRenderer;
    public GameObject choiceButtonPrefab;
    public Transform choiceParent;

    Dictionary <string, DialogueNode> dialogue;
    public DialogueNode currentNode;
    int lineIndex = 0;
    public bool waitingForChoice = false;
    
    Dictionary<string, DialogueNode> savedNodes;
    public static DialogueManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadDialogue(Dictionary<string, DialogueNode> data, string start)
    {
        dialogue = data;
        savedNodes = data;
        currentNode = dialogue[start];
        lineIndex = 0;
     
        DisplayLine();
    }

    public void DisplayLine()
    {
        textRenderer.Say($"{currentNode.speaker}: {currentNode.lines[lineIndex]}");

        if (currentNode.choices != null && lineIndex == currentNode.lines.Length - 1)
        {
            waitingForChoice = true;
            DisplayChoices();
        }
    }

    void DisplayChoices()
    {
{
    int totalChoices = currentNode.choices.Count;
    float spacingX = 100f;     // distance between buttons
    float baseY = -50f;         // pixels from bottom of parent

    int i = 0;
    foreach (var kvp in currentNode.choices)
    {
        // Instantiate a simple empty GameObject as hit area
        GameObject hitObj = new GameObject(kvp.Key);
        hitObj.transform.SetParent(choiceParent, false);

        // Add RectTransform
        RectTransform rt = hitObj.AddComponent<RectTransform>();
        rt.sizeDelta = new UnityEngine.Vector2(160, 40); // size of clickable area
        float offset = (i - (totalChoices - 1) / 2f) * spacingX;
        rt.anchoredPosition = new UnityEngine.Vector2(offset, baseY);

        // Add CanvasRenderer and Button for UI clicks
        hitObj.AddComponent<CanvasRenderer>();
        UnityEngine.UI.Button btn = hitObj.AddComponent<UnityEngine.UI.Button>();
        string choiceCopy = kvp.Key;
        btn.onClick.AddListener(() => OnChoiceSelected(choiceCopy));

        // Add TextMeshProUGUI for label
        TextMeshProUGUI tmp = new GameObject("Text").AddComponent<TextMeshProUGUI>();
        tmp.transform.SetParent(hitObj.transform, false);
        tmp.text = kvp.Key;
        tmp.fontSize = 18;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.rectTransform.sizeDelta = rt.sizeDelta;

        // Track text object if needed
        textRenderer.textObjects[kvp.Key] = tmp.gameObject;

        i++;
    }
    waitingForChoice = true;
    
}

    }

    void Choose(string choice)
    {
        if (!currentNode.choices.ContainsKey(choice)) return;

        waitingForChoice = false;
        foreach (var kvp in currentNode.choices)
        {
            textRenderer.DeleteText(kvp.Key);            
        }

        string nextNodeId = currentNode.choices[choice];
        currentNode = dialogue[nextNodeId];
        lineIndex = 0;
        DisplayLine();
    }

    void NextNode()
    {
        currentNode.onComplete?.Invoke();

        if (currentNode.waitForExternalEvent)
        {

         return;   
        }

        if (currentNode.next != null)
        {
            currentNode = dialogue[currentNode.next];
            lineIndex = 0;
            DisplayLine();
            return;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (waitingForChoice) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        return;
        if (lineIndex < currentNode.lines.Length - 1)
        {
            lineIndex++;
            DisplayLine();
            return;
        }
        if (currentNode.choices == null){
        NextNode();
        }
    }
    
    public void ContinueAfterMiniGame()
{
    // Continue from Q4 node
    LoadDialogue(savedNodes, "Q4");
}

    public void OnChoiceSelected(string choice)
    {
        if (waitingForChoice)
            Choose(choice);
    }
}
