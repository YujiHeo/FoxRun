using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float destroyDelay = 0.1f; //충돌했을 시 아이템 오브젝트가 사라질 때까지의 지연시간
    private bool isDestroyed = false;

    protected abstract void ApplyEffect(Player player);


    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player") && !isDestroyed)
        {
            
            if(player !=null)
            {
                ApplyEffect(player);
                Destroy(gameObject, destroyDelay);
            }
            
        }
    }
}
