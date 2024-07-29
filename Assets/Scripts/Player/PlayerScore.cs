using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerScore : MonoBehaviour
{

    [Tooltip("Текст счёта игрока")]
    public TMPro.TextMeshPro scoreText;

    [Tooltip("Текст хп игрока")]
    public TMPro.TextMeshPro hpText;

    [Tooltip("Текст комбо бонуса")]
    public TMPro.TextMeshPro comboBonusText;

    [Tooltip("Максимальное хп игрока")]
    public int maxHp;

    private int hp;

    private uint score = 0;

    private uint comboBonus = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHp;
        scoreText.text = "Score: " + score;
        hpText.text = "HP: " + hp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddScore(uint points)
    {
        score += points + comboBonus;
        comboBonus++;
        if (comboBonus > 1)
            comboBonusText.text = "Combo! " + comboBonus;
        scoreText.text = "Score: " + score;
    }

    private void Damaged(int damage)
    {
        if (hp - damage <= 0)
            SceneManager.LoadScene("You Died");

        hp -= damage;
        comboBonus = 0;
        comboBonusText.text = "";
        hpText.text = "HP: " + hp;
    }
}
