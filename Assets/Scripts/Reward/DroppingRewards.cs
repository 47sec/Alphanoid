using System.Collections.Generic;
using UnityEngine;

public class DroppingRewards : MonoBehaviour
{
    [Tooltip("������ ������ ��� ���������")]
    public List<Reward> rewards = new List<Reward>();

    [Tooltip("����� ������ � ��������� ������� �������")]
    public bool randomRewards;

    [ConditionalHide(nameof(randomRewards), hideInInspector: true)]
    [Tooltip("��� ����� ����� ����������� ������� ������ �������")]
    public float dropRate;

    [ConditionalHide(nameof(randomRewards), hideInInspector: true)]
    [Tooltip("���� ��������� ������� ��� ������� ������ (0-1)")]
    public float chance;

    [Tooltip("����� ������� ��� ����������")]
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
        // ����� ������� � ����� Rewards, ����� �������� ��� � ��������
        GameObject rewardsList = GameObject.FindGameObjectWithTag("Rewards");

        // ��� �������� ���������� ���� ������ ��� �������� �����
        if (rewardsList == null)
            return;

        Vector2 bounds = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2);

        // ��������� ����� � �������
        Vector2 randomPos = Vector2.Lerp(bounds, new Vector2(bounds.x + transform.localScale.x, bounds.y), Random.value);

        // ��������� �������
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
        // ������� ������ � ��������� �� ������
        if (Time.time > nextDropTime)
        {
            nextDropTime += dropRate;

            if (Random.value <= chance)
            {
                dropReward();
            }
        }
    }

    // ��� ����������� �������
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
