using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int numberOfQuestionShown = 0;
    int numberOfQuestionCorrectlyAnswered = 0;
    public int score;

    public int GetNumberOfQuestionShown()
    {
        return numberOfQuestionShown;
    }

    public void IncrementNumberOfQuestionShown()
    {
        numberOfQuestionShown++;
    }

    public int GetNumberOfQuestionCorrectlyAnswered()
    {
        return numberOfQuestionCorrectlyAnswered;
    }

    public void IncrementnumberOfQuestionCorrectlyAnswered()
    {
        numberOfQuestionCorrectlyAnswered++;
    }

    public void CalculateScore()
    {
        score = Mathf.RoundToInt(numberOfQuestionCorrectlyAnswered / (float)numberOfQuestionShown * 100);
    }



}
