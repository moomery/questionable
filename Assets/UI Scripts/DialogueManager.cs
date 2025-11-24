using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Numerics;
using TMPro;
using UnityEngine.UI;
// Make sure this is at the top



public class DialogueManager: MonoBehaviour
{
    public TextRenderer textRenderer;
    public GameObject choiceButtonPrefab;
    public Transform choiceParent;
    public TMP_FontAsset choiceFont;

    Dictionary <string, DialogueNode> dialogue;
    public DialogueNode currentNode;
    int lineIndex = 0;
    public bool waitingForChoice = false;
    
    Dictionary<string, DialogueNode> savedNodes;


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

    public void DisplayChoices()
{
   if (currentNode.choices == null || currentNode.choices.Count == 0) return;

    float baseY = -50f; // vertical position
    float spacing = 10f; // horizontal spacing between buttons

    List<RectTransform> buttonRects = new List<RectTransform>();

    // First create buttons and let them auto-size
    foreach (var kvp in currentNode.choices)
    {
        GameObject buttonObj = new GameObject(kvp.Key);
        buttonObj.transform.SetParent(choiceParent, false);

        var btn = buttonObj.AddComponent<UnityEngine.UI.Button>();
        var img = buttonObj.AddComponent<UnityEngine.UI.Image>();
        img.color = new Color(1f, 1f, 1f, 0.1f);

        TextMeshProUGUI tmp = new GameObject("Text").AddComponent<TextMeshProUGUI>();
        tmp.transform.SetParent(buttonObj.transform, false);
        tmp.text = kvp.Key;
        tmp.fontSize = 18;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.raycastTarget = false;
        if (choiceFont != null) tmp.font = choiceFont;

        // Auto-size button to text
        var fitter = buttonObj.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        var layout = buttonObj.AddComponent<HorizontalLayoutGroup>();
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.padding = new RectOffset(10, 10, 5, 5);

        RectTransform rt = buttonObj.GetComponent<RectTransform>();
        rt.pivot = new UnityEngine.Vector2(0.5f, 0.5f);

        buttonRects.Add(rt);

        // Click listener
        string choiceCopy = kvp.Key;
        btn.onClick.AddListener(() => OnChoiceSelected(choiceCopy));

        textRenderer.textObjects[kvp.Key] = tmp.gameObject;
    }

    // Position buttons evenly based on widths
    float totalWidth = spacing * (buttonRects.Count - 1);
    foreach (var rt in buttonRects)
        totalWidth += rt.rect.width;

    float startX = -totalWidth / 2f;

    foreach (var rt in buttonRects)
    {
        float halfWidth = rt.rect.width / 2f;
        rt.anchoredPosition = new UnityEngine.Vector2(startX + halfWidth, baseY);
        startX += rt.rect.width + spacing;
    }

    waitingForChoice = true;
    


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
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
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
