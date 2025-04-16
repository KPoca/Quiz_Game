using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswerQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 7f;
    float timerValue;
    public bool loadNextQuestion;
    public bool isAnsweringQuestion;
    public float fillFraction;

    void Start()
    {
        timerValue = timeToAnswerQuestion;
        isAnsweringQuestion = true;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancleTimer(){
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion) {
            if (timerValue > 0) 
            {
                fillFraction = timerValue / timeToAnswerQuestion;
            } else {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
        } else {
            if (timerValue > 0) {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            } else {
                timerValue = timeToAnswerQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }    
        }
    }

    
}
