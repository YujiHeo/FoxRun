using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Hamburger : Item
{
    public int scoreValue; //ÀÌ ¾ÆÀÌÅÛ È¹µæ ½Ã ¾ò°ÔµÇ´Â Á¡¼ö

    protected override void ApplyEffect(Player player)
    {
        Debug.Log($"{scoreValue}Á¡ È¹µæ!!");
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.AddScore(scoreValue);    
    }
}
