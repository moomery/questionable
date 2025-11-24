using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class DialogueManager: MonoBehaviour
{
    public TextRenderer textRenderer;

    Dictionary <string, DialogueNode> dialogue;
    DialogueNode currentNode;
    int lineIndex = 0;

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
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        return;
        if (lineIndex < currentNode.lines.Length - 1)
        {
            lineIndex++;
            DisplayLine();
            return;
        }
        NextNode();
    }
}
