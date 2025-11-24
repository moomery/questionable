using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject spiderWoman; // Assign the already existing spider in the scene
    void Start()
    {
        // Ensure spider starts inactive
        if (spiderWoman != null)
            spiderWoman.SetActive(false);

        var d = new Dictionary<string, DialogueNode>()
        {
            {
                "Player_1", new DialogueNode()
                {
                    speaker = "Player",
                    lines = new string[]
                    {
                        "Where... Am I?",
                        "I remember I was walking home from work.", 
                        "and then I was suddenly hit by something above me.",
                        "I can't move my arms or legs, I'm tied up!",
                        "I thought I remembered a voice before I passed out but who was it?"
                    },
                    next = "Spider_1"
                }
            },
            {
                "Spider_1", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[]
                    {
                        "You're finally awake.",
                        "I was starting to get bored.",
                        "Are you ready to begin playing my game now?"
                    },
                    choices = new Dictionary<string, string>()
                    {
                        {"Yes", "SPIDER_FORCE"},
                        {"No", "SPIDER_FORCE"}
                    }
                }
            },
            {
                "SPIDER_FORCE", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[]
                    {
                        "Oh, that wasn't actually a choice.",
                        "It was supposed to be, like, rhetorical?",
                        "Like for real I could care less what you answered with"
                    },
                    next = "SPIDER_FINAL"
                }
            },
            {
                "SPIDER_FINAL", new DialogueNode()
                {
                    speaker = "Rachne",
                    lines = new string[]
                    {
                        "Uhhh...",
                        "Yeah let's go play my game or whatever"   
                    },
                    onComplete = () =>
                    {
                        // Hide spider before loading next scene
                        if (spiderWoman != null)
                            spiderWoman.SetActive(false);

                        SceneManager.LoadScene("PhaseA");
                    }
                }
            }
        };

        dialogueManager.LoadDialogue(d, "Player_1");
        StartCoroutine(ActivateSpiderWhenNeeded());
    }

    IEnumerator ActivateSpiderWhenNeeded()
    {
        while (true)
        {
            if (dialogueManager.currentNode != null && dialogueManager.currentNode.speaker == "Rachne")
            {
                if (spiderWoman != null && !spiderWoman.activeSelf)
                {
                    // Activate spider
                    spiderWoman.SetActive(true);

                    // Optional: reposition if needed
                    Vector3 pos = spiderWoman.transform.position;
                    pos.x = 40f; // tweak as needed
                    pos.y = 0; // tweak as needed
                    spiderWoman.transform.position = pos;
                }
            }
            yield return null;
        }
    }
}
