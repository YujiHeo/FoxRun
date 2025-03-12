using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Hamburger : Item
{
    public int scoreValue; //�� ������ ȹ�� �� ��ԵǴ� ����

    protected override void ApplyEffect(Player player)
    {
        Debug.Log($"{scoreValue}�� ȹ��!!");
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.AddScore(scoreValue);    
    }
}
