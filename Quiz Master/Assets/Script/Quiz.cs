using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answer")]
    [SerializeField] GameObject[] choices;
    int correctAnswerIndex = 0;
    bool hasAnsweredEarly;

    [Header("Button")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerSprite;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    Slider progressBar;
    
    public bool gameFinished = false;

    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        progressBar = FindAnyObjectByType<Slider>();
        InitiateProgressBar();
    }


    void Update()
    {
        timerSprite.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion) {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false; 
        } else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) {
            ShowAnswer(-1);
            SetButtonsState(false);
        } else if (Input.GetMouseButtonDown(0) && !timer.isAnsweringQuestion) {
            timer.CancleTimer();
        }
    }


    // Display the question and choices
    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < choices.Length; i++) {
            TextMeshProUGUI buttonText = choices[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }


    // go to next question upon finishing one
    private void GetNextQuestion()
    {
        if (questions.Count > 0){
            SetButtonsState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementNumberOfQuestionShown();
            progressBar.value++;
        } else {
            gameFinished = true;
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }
    }
    // Do things when a choice is selected
    public void OnAnswerSelected(int index)
    {
        ShowAnswer(index);

        // disable the buttons after a choice have been selected
        SetButtonsState(false);
        
        // Prematurely set the timer to its next phase
        timer.CancleTimer();

        hasAnsweredEarly = true;
    }


    private void ShowAnswer(int index) {
        // On correct choice
        if (index == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Correct!";
            Image buttonImage = choices[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementnumberOfQuestionCorrectlyAnswered();
        } 
        
        // On incorrect choice
        else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswerText = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "The correct answer is: " + correctAnswerText;
            Image buttonImage = choices[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        
        scoreKeeper.CalculateScore();
        scoreText.text = "Score: " + scoreKeeper.score + "%";
        
    }

    // change buttons state as needed
    private void SetButtonsState(bool state)
    {
        for (int i = 0; i < choices.Length; i++){
            choices[i].GetComponent<Button>().interactable = state;
        }
    }

    private void SetDefaultButtonSprite()
    {
        for (int i = 0; i < choices.Length; i++) {
            choices[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    private void InitiateProgressBar()
    {
        progressBar.maxValue = questions.Count;
        progressBar.minValue = 0;
        progressBar.value = 0;
    }
}
