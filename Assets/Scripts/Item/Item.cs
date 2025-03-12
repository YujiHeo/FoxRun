using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float destroyDelay = 0.1f;
    private bool isDestroyed = false;

    protected abstract void ApplyEffect(/*player매개변수*/);


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !isDestroyed)
        {
            /*
            if(player !=null)
            {
                ApplyEffect(player)
                Destroy(gameObject, destroyDelay);
            }
            */
        }
    }
}
