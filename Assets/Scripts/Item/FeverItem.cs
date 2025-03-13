using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FeverItem : Item
{
    public int scoreValue;
    public float duration = 5f;

    protected override void ApplyEffect(Player player)
    {
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.AddScore(scoreValue);

        player.StartCoroutine(player.StartFeverTime(duration));
    }
}
