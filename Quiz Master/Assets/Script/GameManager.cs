using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    Endscreen endscreen;

    void Start()
    {
        quiz = FindAnyObjectByType<Quiz>();
        endscreen = FindFirstObjectByType<Endscreen>();

        quiz.gameObject.SetActive(true);
        endscreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.gameFinished)
        {
            // Disable quiz and enable endscreen
            quiz.gameObject.SetActive(false);
            endscreen.gameObject.SetActive(true);
            endscreen.ShowCongratMessage();  // Ensure the congrat message is shown when the end screen is active
            Debug.Log("game finished!");
        }
    }

    public void OnReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
