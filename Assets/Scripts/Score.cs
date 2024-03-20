using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int correctScore = 0;
    int questionSeen = 0;

    public int GetCorrectScore()
    {
        return correctScore;
    }

    public void IncreaseCorrectScore()
    {
        correctScore++;
    }
    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public void IncreaseQuestionSeen()
    {
        questionSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(((float)correctScore / questionSeen) * 100);
    }
}
