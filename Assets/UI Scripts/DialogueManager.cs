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
    float spacingX = 5f; // distance between choice objects
    float centerY = 0f;  // vertical position of all choices
    int i = 0;

    foreach (var kvp in currentNode.choices)
    {
        textRenderer.SayOnObject(choiceParent.gameObject, kvp.Key, kvp.Key, 18);
        GameObject choiceObj = textRenderer.textObjects[kvp.Key];

        // Add a collider if it doesn't exist
        if (choiceObj.GetComponent<BoxCollider2D>() == null)
        {
            var col = choiceObj.AddComponent<BoxCollider2D>();
            var tm = choiceObj.GetComponent<TextMeshPro>();
            col.size = new UnityEngine.Vector2(tm.preferredWidth + 20, tm.preferredHeight + 10);
        }

        // Position choices horizontally like in your QuizManager
        UnityEngine.Vector3 pos = new UnityEngine.Vector3(
            spacingX * (i - (totalChoices - 1) / 2f),
            centerY,
            0
        );
        choiceObj.transform.localPosition = pos;

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
