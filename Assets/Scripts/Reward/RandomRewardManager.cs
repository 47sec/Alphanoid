using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class RandomRewardManager : MonoBehaviour
{
    [Tooltip("—писок наград дл€ рандомного выпадени€")]
    public List<CustomRewardScript> rewards = new List<CustomRewardScript>();

    [Tooltip(" ак часто будет происходить попытка спавна награды")]
    public float dropRate;

    [Tooltip("Ўанс по€влени€ награды при попытке спавна (0-1)")]
    public float chance;

    private float nextDropTime = 0.0f;

    void Start()
    {
        fixChances();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextDropTime)
        {
            nextDropTime += dropRate;


            if (Random.value <= chance)
            {
                dropReward();
            }
        }
    }

    void dropReward()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 randomPos = Vector2.Lerp(bounds, new Vector2(-bounds.x, bounds.y), Random.value);

        float rnd = Random.value;
        foreach (var reward in rewards)
        {
            if (rnd < reward.relativeChance)
            {
                Instantiate(reward.gameObject, transform).transform.position = randomPos;
                return;
            }
            rnd -= reward.relativeChance;
        }

        Instantiate(rewards[0].gameObject, transform).transform.position = randomPos;
    }

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
