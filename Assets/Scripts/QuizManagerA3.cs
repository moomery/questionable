using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizDialogueA3 : MonoBehaviour
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
                    lines = new string[] { "Wrong! This is just dumb, I seriously thought no one would pick this." },
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
                    lines = new string[] { "Good! It's kinda weird that you know that" },
                    next = "Q7"
                }
            },
            {
                "Q6_WRONG", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "Nope! Idiot!" },
                    next = "Q7"
                }
            },

            // 2nd Phase B (optional mini-game)
            {
                "Q7", new DialogueNode()
                {
                    speaker = "Spider",
                    lines = new string[] { "If you win this time maybe you can defeat Fart, the mighty!" },
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
                    speaker = "Rachne",
                    lines = new string[] { "What was the name of the first spider you shot flies with?" },
                     choices = new Dictionary<string,string>()
                    {
                        { "Jerry", "Q8_CORRECT" },
                        { "Bob", "Q8_WRONG" }
                    },
                }
            },
            {
                "Q8_CORRECT", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[] { "Good! You have a soul" },
                    next = "Q9"
                }
            },
            {
                "Q8_WRONG", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[] { "Nope! How rude of you to not remember him after he so valiantly died after that mini game!" },
                    next = "Q9"
                }
            },
            {
                "Q9", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[] { "What's the name of the big scary monster that lives further in the cave?" },
                    choices = new Dictionary<string,string>()
                    {
                        { "Fart", "Q9_CORRECT" },
                        { "Billy", "Q9_WRONG" }
                    },
                }
            },
            {
                "Q9_CORRECT", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[] { "Good!" },
                    next = "Q10"
                }
            },
            {
                "Q9_WRONG", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[] { "Nope!" },
                    next = "Q10"
                }
            },
            {
                "Q10", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[] { "Oh No! Fart is here to kill us! Save us!" },
                    onComplete = () =>
                    {   // Victory
                     var spiderWoman = GameObject.Find("boss_0");
                        if (spiderWoman != null)
                        Destroy(spiderWoman);

                        var audioObj = GameObject.Find("MusicPhaseA3");
                        if (audioObj != null)
                        Destroy(audioObj);
                        
                        SceneManager.LoadScene("PhaseC");
                    }
                }
            }
            
        };
        dialogueManager.LoadDialogue(nodes, "Q8");
    }
    
}
