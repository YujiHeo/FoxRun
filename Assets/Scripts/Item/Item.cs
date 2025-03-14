using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float destroyDelay = 0.1f; //�浹���� �� ������ ������Ʈ�� ����� �������� �����ð�
    private bool isDestroyed = false;

    protected abstract void ApplyEffect(PlayerCondition player);


    private void OnTriggerEnter(Collider collision)
    {
        PlayerCondition player = collision.GetComponent<PlayerCondition>();
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
