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
    public int answerCount = 3;

    public float snapSpeed = 12f;

    private UnityEngine.Vector2 targetPosition;
    public float targetAngle;

    //game object answer objects
    public GameObject answerPrefab;
    public GameObject[] answers;

        void SetIndex(int index)
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
        answers = new GameObject[answerCount];

        float startAngle = 0f;
        float endAngle = 180f;
        float angleStep = (endAngle - startAngle)/(answerCount-1);

        for (int i = 0; i < answerCount; i++)
        {
            float angleDeg = startAngle + angleStep * i;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            UnityEngine.Vector2 pos = new UnityEngine.Vector2(center.x + Mathf.Cos(angleRad)* radius/1.5f , (center.y + Mathf.Sin(angleRad) * radius/3 ));

            GameObject obj = Instantiate(answerPrefab, pos, UnityEngine.Quaternion.identity);
            answers[i] = obj;
        }
        SetIndex(0);
        transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = UnityEngine.Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * snapSpeed);
        // transform.rotation = UnityEngine.Quaternion.Lerp(transform.rotation, UnityEngine.Quaternion.Euler(0,0, targetAngle), Time.deltaTime * snapSpeed);

    }
}


