using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScreenText;
    Score score;

    void Awake()
    {
        score = FindObjectOfType<Score>();
    }
    public void ShowFinalScore()
    {
        finalScreenText.text = "Congratulation! \n Your score is " + score.CalculateScore() + "%";
    }
}
