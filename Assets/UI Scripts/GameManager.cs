using System;
using UnityEngine;


public class Question
{
    public string question; 
    public string[] choices;

    public int indexOfCorrectAnswer;



    string[] banter;
    public Question(string question, string[] choices, int indexOfCorrectAnswer, string[] banter)
    {
        this.question = question;
        this.choices = choices;
        this.indexOfCorrectAnswer = indexOfCorrectAnswer;
        this.banter = banter;
    }
}
public class GameManager : MonoBehaviour
{
    public TextRenderer textRenderer;
    int currentQuestion = 0;
    Boolean needsQuestion = true;

    public Question[] questions => new Question[]
    {
        new Question(
            "What color is the sky?",
            new string [] {"Red", "Blue", "None"},
            1,
            new string [] {"What?", "CorrecT!", "What?"}

        )

    };


    public GameObject SelectorBody;
    SelectorBody s;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          //selector positions
    float SelectorX = 0.0f;
    float SelectorZ = 0.0f;
    float SelectorY = (-5.0f)*19;
    Vector3 spawnPosition = new Vector3 (SelectorX, SelectorY, SelectorZ);

     Instantiate(SelectorBody, spawnPosition, Quaternion.identity);
     s = SelectorBody.GetComponent<SelectorBody>();


    }

    // Update is called once per frame
    void Update()
    {
        if(needsQuestion)
        {
            // Load the text into the default text bos with Say function,
                 //load questio
            textRenderer.Say(questions[currentQuestion].question);

            // Load the answer options into the text box
            s.PopulateChoices(questions[currentQuestion].choices);

            needsQuestion = false;
        }
        else
        {
            // Wait for a response......
        }
    }
}
