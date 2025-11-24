using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    { 
        var nodes = new Dictionary<string, DialogueNode>()
        {
            // First Phase A
            {
                "Q1", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "What color is the sky?" },
                    choices = new Dictionary<string,string>()
                    {
                        { "Red", "Q1_WRONG" },
                        { "Blue", "Q1_CORRECT" },
                        { "None", "Q1_WRONG" }
                    }
                }
            },
            {
                "Q1_CORRECT", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Correct!" },
                    next = "Q2"
                }
            },
            {
                "Q1_WRONG", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Wrong!" },
                    next = "Q2"
                }
            },
            {
                "Q2", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Do Stalactites hang on the ceiling or emerge from the floor?" },
                    choices = new Dictionary<string,string>()
                    {
                        { "Ceiling", "Q2_CORRECT" },
                        { "Floor", "Q2_WRONG" }
                    }
                }
            },
            {
                "Q2_CORRECT", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Good job!" },
                    next = "Q3"
                }
            },
            {
                "Q2_WRONG", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Incorrect!" },
                    next = "Q3"
                }
            },

            // Phase B: Mini-game
            {
                "Q3", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Time for a mini-game!" },
                    waitForExternalEvent = true,
                    onComplete = () =>
                    {
                        var spiderWoman = GameObject.Find("boss_0");
                        if (spiderWoman != null)
                        Destroy(spiderWoman);
                        // Load PhaseB additively
                        {
                        if (!SceneManager.GetSceneByName("PhaseB").isLoaded)
                        SceneManager.LoadScene("PhaseB", LoadSceneMode.Additive);
                        
                        };
                        Debug.Log("Number of scenes:" + SceneManager.sceneCount);
                    }
                }
            },

            // 2nd Phase A
            {
                "Q4", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "What are baby spiders called?" },
                    choices = new Dictionary<string,string>()
                    {
                        { "Spiderlings", "Q4_CORRECT" },
                        { "Spidlets", "Q4_WRONG" }
                    }
                }
            },
            {
                "Q4_CORRECT", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Correct!" },
                    next = "Q5"
                }
            },
            {
                "Q4_WRONG", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Wrong!" },
                    next = "Q5"
                }
            },
            {
                "Q5", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "What is the most venomous spider on the planet? " },
                    choices = new Dictionary<string,string>()
                    {
                        { "Atrax robustus", "Q5_CORRECT" },
                        { "Latrodectus mactans", "Q5_WRONG" }
                    }
                }
            },
            {
                "Q5_CORRECT", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Correct!" },
                    next = "Q6"
                }
            },
            {
                "Q5_WRONG", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Incorrect!" },
                    next = "Q6"
                }
            },
            {
                "Q6", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "What are the 'hairs' on many species of spider called?" },
                    choices = new Dictionary<string,string>()
                    {
                        { "Setae", "Q6_CORRECT" },
                        { "Fur", "Q6_WRONG" }
                    }
                }
            },
            {
                "Q6_CORRECT", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Good!" },
                    next = "Q7"
                }
            },
            {
                "Q6_WRONG", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Nope!" },
                    next = "Q7"
                }
            },

            // 2nd Phase B (optional mini-game)
            {
                "Q7", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Time for another mini-game!" },
                    waitForExternalEvent = true,
                    onComplete = () =>
                    {
                        // Load PhaseB additively again
                        SceneManager.LoadSceneAsync("PhaseB");
                    }
                }
            },

            // Last Phase A
            {
                "Q8", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "What was the name of the first spider you shot flies with?" },
                    next = "Q9"
                }
            },
            {
                "Q9", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Again, how many pixels of hair do I have?" },
                    next = "Q10"
                }
            },
            {
                "Q10", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Final Question: What kind of spider am I?" },
                    onComplete = () =>
                    {   // Victory
                        dialogueManager.textRenderer.Say("You Win!");
                    }
                }
            }
            
        };
        dialogueManager.LoadDialogue(nodes, "Q1");
    }
    
}
