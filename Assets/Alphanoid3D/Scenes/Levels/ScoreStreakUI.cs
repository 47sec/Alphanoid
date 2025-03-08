using System.Text;
using TMPro;
using UnityEngine;
public class ScoreStreakUI : MonoBehaviour
{
    public Player player;
    private TMP_Text _text;
    private string _preText;

    private void OnEnable()
    {
        player.score.OnStreakPointsChanged += ChangeStreakScore;
    }
    private void OnDisable()
    {
        player.score.OnStreakPointsChanged -= ChangeStreakScore;
    }
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _preText = _text.text;

        ChangeStreakScore(0);
    }
    private void Update()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_preText);
        if (player.score.Streak > 0)
        {
            sb.Append(player.score.Streak + "x -");
            sb.Append(player.score.StreakPoints + "-  ~");
            sb.Append(player.score._streakTimer.Count);
        }
        else
            sb.Append("---");

        _text.SetText(sb);
    }
    void ChangeStreakScore(int score)
    {

    }
}
