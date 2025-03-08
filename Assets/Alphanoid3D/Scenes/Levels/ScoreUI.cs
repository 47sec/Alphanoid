using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Player player;
    private TMP_Text _text;
    private string _preText;

    private void OnEnable()
    {
        player.score.OnScoreChanged += ChangeScore;
    }
    private void OnDisable()
    {
        player.score.OnScoreChanged -= ChangeScore;
    }
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _preText = _text.text;

        ChangeScore(0);
    }
    void ChangeScore(int score)
    {
        _text.SetText(_preText + player.score.Points);
    }
}
