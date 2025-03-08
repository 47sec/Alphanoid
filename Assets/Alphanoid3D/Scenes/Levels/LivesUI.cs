using TMPro;
using UnityEngine;
public class LivesUI : MonoBehaviour
{
    public Player player;
    private TMP_Text _text;
    private string _preText;
    private void OnEnable()
    {
        player.health.OnHealthChange += ChangeHealth;
    }
    private void OnDisable()
    {
        player.health.OnHealthChange -= ChangeHealth;
    }
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _preText = _text.text;

        ChangeHealth(0);
    }
    void ChangeHealth(int score)
    {
        _text.SetText(_preText + player.health.Health);
    }
}
