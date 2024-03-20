using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question" , fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
        [TextArea(2,4)] 
        [SerializeField] string question = "Enter new question text here";
        [SerializeField] string[] answer = new string[4];
        [SerializeField] int correctAnswerIndex;

        public int GetCorrectAnswerIndex()
        {
                return correctAnswerIndex;
        }
        public string GetAnswer(int index)
        {
                return answer[index];
        }
        public string GetQuestion()
        {
                return question;
        }
}
