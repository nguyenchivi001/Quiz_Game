using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Sprites")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite incorrectAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score score;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isCompleteGame;
    void Awake()
    {
        timer = FindObjectOfType<Timer>(); 
        score = FindObjectOfType<Score>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue) 
            {
                isCompleteGame = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && timer.AnsweredQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnCorrectAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);

        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + score.CalculateScore() + "%";

    }

    void DisplayAnswer(int index)
    {
        if (currentQuestion != null) 
        {
            Image buttonImage;
            if(index == currentQuestion.GetCorrectAnswerIndex())
            {
                questionText.text = "Correct answer!";
                buttonImage = answerButtons[index].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
                score.IncreaseCorrectScore();
            }
            else
            {
                correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
                string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
                questionText.text = "The correct answer is: \n" + correctAnswer;
                if(index >= 0  && index <= 3)
                {
                    buttonImage = answerButtons[index].GetComponent<Image>();
                    buttonImage.sprite = incorrectAnswerSprite;
                }
                buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
            
            }
            if(questions.Contains(currentQuestion))
            {
                questions.Remove(currentQuestion);
            }
        }
    }
    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            score.IncreaseQuestionSeen();
            progressBar.value++;
        }
    }


    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count); 
        currentQuestion = questions[index]; 
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for(int i = 0 ; i < answerButtons.Length ; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for(int i = 0 ; i < answerButtons.Length ; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite()
    {
        for(int i = 0 ; i < answerButtons.Length ; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
