using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockManager blockManager;

    public List<CustomRewardScript> rewards = new List<CustomRewardScript>();

    [HideInInspector]
    public float relativeChance;

    [Tooltip("Модификатор шанса спавна блока")]
    public float chanceMod;

    [Tooltip("Количество ударов по блоку для разрушения")]
    public int blockHp;

    [Tooltip("Количество очков за разрушение блока")]
    public uint blockPoints;

    [Tooltip("Ускорение мяча при столкновении с блоком (1 = 100%)")]
    public float blockAcceleration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixChances();
        relativeChance = chanceMod;
        blockManager = GameObject.FindWithTag("BlockManager").GetComponent<BlockManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setRelativeChance(float sum)
    {
        relativeChance = chanceMod / sum;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            MoveBall ball = collision.gameObject.GetComponent<MoveBall>();
            Rigidbody2D ballRb = ball.gameObject.GetComponent<Rigidbody2D>();

            var customScript = transform.GetComponent<CustomBlockScript>();

            if (customScript != null)
            {
                customScript.CollisionUpdate(collision);
            }

            blockHp--;

            ball.ChangeSpeed(blockAcceleration);

            if (blockHp <= 0)
            {
                if (rewards.Count > 0)
                    dropReward();
                blockManager.DestroyBlock(this);
                ball.gameObject.SendMessage("Scored", blockPoints);
            }
        }
    }

    private void dropReward()
    {
        float rnd = Random.value;
        foreach (var reward in rewards)
        {
            if (rnd < reward.relativeChance)
            {
                Instantiate(reward.gameObject, GameObject.FindGameObjectWithTag("RandomRewardManager").transform).transform.position = transform.position;
                return;
            }
            rnd -= reward.relativeChance;
        }

        Instantiate(rewards[0].gameObject, GameObject.FindGameObjectWithTag("RandomRewardManager").transform).transform.position = transform.position;
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
