using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FeverItem : Item
{
    public int scoreValue;
    public float duration = 5f;

    protected override void ApplyEffect(PlayerCondition player)
    {
        player.isFeverTime = true;

        GameManager.instance.AddScore(scoreValue);
        GameManager.instance.feverTimeScore = 2;

        player.StartCoroutine(player.StartFeverTime(player, duration));
        UIManager.instance.UpdateFeverDuration(duration);
    }
}
