using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : Item
{
    public int scoreValue;
    public float duration = 5f;


    protected override void ApplyEffect(PlayerCondition player)
    {
        GameManager.instance.AddScore(scoreValue);

        player.StartInvin(duration);
    }
}
