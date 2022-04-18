using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionName : MonoBehaviour
{
    private Text QuestionDisplay;
  

    public static QuestionName S;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Qustion Name is working");

        QuestionDisplay = GetComponentInChildren<Text>();
        QuestionDisplay.text = "";

    }

    public void SetText()
    {
        QuestionDisplay.text = Question.S.questionName;
    }
}
