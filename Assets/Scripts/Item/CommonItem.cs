using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CommonItem : Item
{
    public int scoreValue; //이 아이템 획득 시 얻게되는 점수

    protected override void ApplyEffect(PlayerCondition player)
    {

        GameManager.instance.AddScore(scoreValue);

    }
}
