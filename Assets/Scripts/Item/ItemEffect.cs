using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public GameObject itemEffectPrefab;

    public float destroyDelay = 1f; //Hierarchy에 생성된 이펙트가 Destroy 될 때까지의 지연 시간
    private bool isDestroyed;

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !isDestroyed)
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                GameObject effectInstance = PlayEffect();

                //if (effectInstance != null)
                //{
                //    Destroy(effectInstance, destroyDelay);
                //}
            }
        }
    }

    public GameObject PlayEffect()
    {
        if (itemEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);
            return effectInstance;
        }
        return null;
    }
    
}
