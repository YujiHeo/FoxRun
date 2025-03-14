using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : Item
{
    public int scoreValue;
    public float duration = 5f;


    protected override void ApplyEffect(PlayerCondition player)
    {
        player.isInvincibleTime = true;
        PlayerCondition.isMagnet = true;


        GameManager.instance.AddScore(scoreValue);

        player.StartCoroutine(player.StartInvincibleTime(player, duration));
    }
}
