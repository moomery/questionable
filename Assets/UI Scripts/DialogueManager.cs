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
    

    public void LoadDialogue(Dictionary<string, DialogueNode> data, string start)
    {
        dialogue = data;
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
         int i = 0;
    float spacingX = 200f; // horizontal space between choices
    float centerY = 10f;    // vertical position for all choices

    foreach (var kvp in currentNode.choices)
    {
        // Create the text object
        textRenderer.SayOnObject(choiceParent.gameObject, kvp.Key, kvp.Key, 18);
        GameObject choiceObj = textRenderer.textObjects[kvp.Key];

        // Add BoxCollider2D for clicks
        if (choiceObj.GetComponent<BoxCollider2D>() == null)
        {
            var col = choiceObj.AddComponent<BoxCollider2D>();
            var textMesh = choiceObj.GetComponent<TextMeshProUGUI>();
            col.size = new UnityEngine.Vector2(textMesh.preferredWidth + 20, textMesh.preferredHeight + 10);
        }

        // Position choices horizontally
        RectTransform rt = choiceObj.GetComponent<RectTransform>();
        rt.anchorMin = new UnityEngine.Vector2(0.5f, 0.5f);
        rt.anchorMax = new UnityEngine.Vector2(0.5f, 0.5f);
        rt.pivot = new UnityEngine.Vector2(0.5f, 0.5f);

        // Calculate horizontal offset: center them around x = 0
        int totalChoices = currentNode.choices.Count;
        float startX = -spacingX * (totalChoices - 1) / 2f; // starting x for first choice
        rt.anchoredPosition = new UnityEngine.Vector2(startX + spacingX * i, centerY);

        i++;
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
        currentNode.onComplete?.Invoke();

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
    public void OnChoiceSelected(string choice)
    {
        if (waitingForChoice)
            Choose(choice);
    }
}
