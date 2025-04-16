using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter your question here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex = 0;
    
    public string GetQuestion() 
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public string GetCorrectAnswer(int index)
    {
        return answers[index];
    }
    
}


