using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string question;
    public Answer[] answers = new Answer[4];
    public bool answered;
}
