using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Question
{
    public string question; 
    public string[] choices;

    public int indexOfCorrectAnswer;



    public string[] banter;
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
    public string state = "NEEDS_QUESTION";
    

    public Question[] questions => new Question[]
    {
        new Question(
            "What color is the sky?",
            new string [] {"Red", "Blue", "None"},
            1,
            new string [] {"What?", "Correct!", "What?"}

        ),

        new Question(
            "Are you SURE?",
            new string [] {"Yes", "No"},
            1,
            new string [] {"I like your confidence.", "How dare you!"}

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


        if(currentQuestion >= questions.Length)
        {
            textRenderer.Say("You win!");
            state = States_.VICTORY;
        }

        else if(state == States_.NEEDS_QUESTION)
        {
            s.FlushChoices();
            // Load the text
            textRenderer.Say(questions[currentQuestion].question);

            // Load the answer options into the text box
            s.PopulateChoices(questions[currentQuestion].choices, currentQuestion);

            state = States_.IS_ANSWERING;

        }
        else if (state == States_.IS_ANSWERING)
        {
            // Check for answer
            int ans = s.GetAnswer();
            if(ans == -1)
            {
                return;
            }
            state = States_.AWAITING_CONTINUE;
            textRenderer.Say(questions[currentQuestion].banter[ans]);

        }
        if (state == States_.AWAITING_CONTINUE) 
        {

            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                state = States_.NEEDS_QUESTION;  
                currentQuestion += 1;        
            }

        }
    }
}
