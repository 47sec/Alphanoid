using UnityEditor;
using UnityEditor.SceneManagement;
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
        if (score + points >= 16) // Временная фигня, нужна нормальная проверка на победу
            Win();

        score += points + comboBonus;
        comboBonus++;
        if (comboBonus > 1)
            comboBonusText.text = "Combo! " + comboBonus;
        scoreText.text = "Score: " + score;
    }

    private void Damaged(int damage)
    {
        if (hp - damage <= 0)
            Killed();
        hp -= damage;
        comboBonus = 0;
        comboBonusText.text = " ";
        hpText.text = "HP: " + hp;
    }

    private void Killed()
    {
        SceneManager.LoadScene("You Died");
        SceneManager.UnloadSceneAsync("Base");
    }

    private void Win()
    {
        SceneManager.LoadScene("You Win");
        SceneManager.UnloadSceneAsync("Base");
    }

}
