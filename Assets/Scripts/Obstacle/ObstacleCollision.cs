using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           var player = collision.transform.GetComponent<PlayerCondition>();
            player.GetDamage(10);
        }
    }

}
