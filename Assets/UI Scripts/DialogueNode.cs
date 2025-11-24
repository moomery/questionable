using UnityEngine;
using System;
using System.Collections.Generic;


public class DialogueNode
{

     public string speaker;
     public string[] lines;
     //for dialogue like yes or no stuff

     public Dictionary<string, string> choices;
    //for quiz stuff like banter
    public Func<int, string> onAnswer;

    public string next;
    public Action onComplete;

}
