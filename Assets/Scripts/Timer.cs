using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool AnsweredQuestion = true;//đã trả lời câu hỏi 
    
    public float fillFraction;
    public bool loadNextQuestion = false;
    float timerValue;
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(!AnsweredQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                AnsweredQuestion = true;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else 
        {   
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                AnsweredQuestion = false;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
