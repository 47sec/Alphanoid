using UnityEngine;

public class HpRewardScript : CustomRewardScript
{
    [Tooltip("Сила восстановления")]
    public int healAmount;

    public override void Use(Collider2D collision)
    {
        collision.transform.GetComponent<PlayerScore>().addHp(healAmount);
    }
}
