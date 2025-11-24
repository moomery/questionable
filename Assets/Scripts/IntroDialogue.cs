using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject spiderWomanPrefab;
    public Transform spiderSpawn;
    GameObject spawnedSpider;
    void Start()
    {
        
        var d = new Dictionary<string,DialogueNode>()
  
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

                            "I remember I was walking home from work when suddenly this dark shape swooped down on top of me.",

                            "I'm not able to move my arms or legs, I'm tied up!",
                            "I thought I remembered a voice before I passed out but who was it?"
                    },
                        next = "Spider_1"
                }
            },
            
            {
                    "Spider_1", new DialogueNode()
    
                {
                    speaker = "Spider Woman",
                        lines = new string[]
                    {
                            "You're finally awake.",
                            "I was starting to get bored.",
                            "Are you read to begin playing my game now?"
                    },
                        choices = new Dictionary<string, string>()
                    {
                            {"Yes", "SPIDER_FORCE"},
                            {"No", "SPIDER_FORCE"}
                    }
                }
            },
            {"SPIDER_FORCE", new DialogueNode()
                {
                    speaker = "Spider Woman",
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
                    speaker = "Spider Woman",
                    lines = new string[]
                    {
                    "Uhhh...",
                    "Yeah let's go play my game or whatever"   
                    },
                    onComplete = () =>
                    {
                        if (spawnedSpider != null)
                        Destroy(spawnedSpider);
                        SceneManager.LoadScene("PhaseA");
                    }
                }
            }
        };
        dialogueManager.LoadDialogue(d, "Player_1");
        StartCoroutine(SpawnSpiderWoman());
    }
    IEnumerator SpawnSpiderWoman()
    {
        while ( true )
        {
            if(dialogueManager.currentNode != null && dialogueManager.currentNode.speaker == "Spider Woman"){
            if (spawnedSpider == null)
                {
                    spawnedSpider = Instantiate(spiderWomanPrefab, spiderSpawn.position, Quaternion.identity);
                    UnityEngine.Vector3 pos = spawnedSpider.transform.position;
                    pos.y = -70.5f;
                    pos.x = -20.5f;
                    spawnedSpider.transform.position = pos;

                    var sr = spawnedSpider.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.sortingLayerName = "Default";
                        sr.sortingOrder = -200;
                    }
                    yield break;
                }
            }
            yield return null;
        }
        
        

    }
}
