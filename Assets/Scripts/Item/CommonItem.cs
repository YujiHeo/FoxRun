using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CommonItem : Item
{
    public int scoreValue; //�� ������ ȹ�� �� ��ԵǴ� ����

    protected override void ApplyEffect(PlayerCondition player)
    {

        GameManager.instance.AddScore(scoreValue);

    }
}
