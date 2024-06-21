using System.Collections.Generic;
using UnityEngine;

public class DroppingRewards : MonoBehaviour
{
    [Tooltip("Список наград для выпадения")]
    public List<Reward> rewards = new List<Reward>();

    [Tooltip("Спавн наград в рандомные моменты времени")]
    public bool randomRewards;

    [ConditionalHide(nameof(randomRewards), hideInInspector: true)]
    [Tooltip("Как часто будет происходить попытка спавна награды")]
    public float dropRate;

    [ConditionalHide(nameof(randomRewards), hideInInspector: true)]
    [Tooltip("Шанс появления награды при попытке спавна (0-1)")]
    public float chance;

    [Tooltip("Спавн награды при разрушении")]
    public bool dropOnDestroy;

    private float nextDropTime = 0.0f;

    void Start()
    {
        fixChances();
    }

    void Update()
    {
        if (randomRewards)
            randomReward();
    }

    private void OnDestroy()
    {
        if (dropOnDestroy)
            dropReward();
    }

    void dropReward()
    {
        // Поиск объекта с тегом Rewards, чтобы записать его в родители
        GameObject rewardsList = GameObject.FindGameObjectWithTag("Rewards");

        // Без проверки выкидывает кучу ошибок при закрытии сцены
        if (rewardsList == null)
            return;

        Vector2 bounds = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2);

        // Рандомная точка в объекте
        Vector2 randomPos = Vector2.Lerp(bounds, new Vector2(bounds.x + transform.localScale.x, bounds.y), Random.value);

        // Рандомная награда
        float rnd = Random.value;
        foreach (var reward in rewards)
        {
            if (rnd < reward.relativeChance)
            {
                Instantiate(reward.gameObject, rewardsList.transform).transform.position = randomPos;
                return;
            }
            rnd -= reward.relativeChance;
        }

        Instantiate(rewards[0].gameObject, rewardsList.transform).transform.position = randomPos;
    }

    private void randomReward()
    {
        // Простой таймер с проверкой на рандом
        if (Time.time > nextDropTime)
        {
            nextDropTime += dropRate;

            if (Random.value <= chance)
            {
                dropReward();
            }
        }
    }

    // Для корректного рандома
    private void fixChances()
    {
        float sum = 0;
        foreach (var reward in rewards)
        {
            sum += reward.chanceMod;
        }

        foreach (var reward in rewards)
        {
            reward.setRelativeChance(sum);
        }

        rewards.Sort((x, y) => y.relativeChance.CompareTo(x.relativeChance));
    }
}
