using System;
using System.Dynamic;
using System.Numerics;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class SelectorBody : MonoBehaviour
{
    [Header("Arc Settings")]
    public UnityEngine.Vector2 center = new UnityEngine.Vector2(0, -4f);
    public float radius = 5f;
    public float answerAmplitude = 2f;

    public float snapSpeed = 12f;

    private UnityEngine.Vector2 targetPosition;
    public float targetAngle;

    //game object answer objects
    public GameObject answerPrefab;
    public GameObject[] answers;

    public TextRenderer textRenderer;

    public Canvas canvas;

        void SetIndex(int answerCount, int index)
        {
            if (answerCount < 1) return;
            if (index < 0)index = 0;
            if(index >= answerCount) index = answerCount - 1;

            float startAngle = 0f;
            float endAngle = 180f;

            float angleStep = (endAngle-startAngle)/(answerCount-1);
            float angle = startAngle + angleStep * index;
            float rad = angle * Mathf.Deg2Rad;


            targetAngle = angle - 90f;
            targetPosition = new UnityEngine.Vector2(center.x + Mathf.Cos(rad) * radius, (center.y + Mathf.Sin(rad)* radius));


        }
        void Start()
    {
    }

    public int GetAnswer()
    {

        for (int i = 0; i < answers.Length; i++)
        {
            if(answers[i].GetComponent<SpriteRenderer>().color == Color.red)
            {
                return i;
            }
        }
        return -1;
    }

    public void FlushChoices()
    {
        foreach (GameObject answer in answers)
        {
            Destroy(answer);
        }
        answers = new GameObject[0];
    }

    public void PopulateChoices(string[] choices, int qnum)
    {
        int answerCount = choices.Length;
        answers = new GameObject[answerCount];

        float startAngle = -20f;
        float endAngle = 200f;
        float angleStep = (endAngle - startAngle)/(answerCount-1);

        for (int i = 0; i < answerCount; i++)
        {
            float angleDeg = startAngle + angleStep * i;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            UnityEngine.Vector2 pos = new UnityEngine.Vector2(center.x + Mathf.Cos(angleRad)* (radius*1.1f) , (center.y + Mathf.Sin(angleRad) * radius/2.5f ));

            GameObject obj = Instantiate(answerPrefab, pos*15, UnityEngine.Quaternion.identity);
            answers[i] = obj;

            textRenderer.SayOnObject(obj, "Q_" + qnum + "A_" + i, choices[i], 15);
            //reparent obj under canvas
            obj.transform.SetParent(canvas.transform, false);
        }
        SetIndex(answerCount, 0);
        transform.position = targetPosition;

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = UnityEngine.Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * snapSpeed);
        // transform.rotation = UnityEngine.Quaternion.Lerp(transform.rotation, UnityEngine.Quaternion.Euler(0,0, targetAngle), Time.deltaTime * snapSpeed);
    }
}


