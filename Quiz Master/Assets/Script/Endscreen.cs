using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Endscreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI congratMessage;
    ScoreKeeper score;

    void Start()
    {
        score = FindAnyObjectByType<ScoreKeeper>();
    }

    public void ShowCongratMessage()
    {
        congratMessage.text = "Congratulation!\nYou got a scored of " + score.score + "%";
    }
}
