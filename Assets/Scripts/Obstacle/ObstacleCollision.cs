using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�浹");
            var player = other.transform.GetComponent<PlayerCondition>();
            if (player != null) Debug.Log("���� �ƴϴ�"); 
            player.GetDamage(1);
        }

    }

}
