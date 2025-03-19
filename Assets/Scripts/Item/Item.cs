using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float destroyDelay = 0.1f; //�浹���� �� ������ ������Ʈ�� ����� �������� �����ð�
    private bool isDestroyed = false;
    private float lastTriggerTime = 0f;  // ���������� ����� �ð�
    private float triggerCooldown = 0.2f;  // ���� ���� (0.2�ʸ��� ����)

    protected abstract void ApplyEffect(PlayerCondition player);


    private void OnTriggerEnter(Collider collision)
    {

        PlayerCondition player = collision.GetComponent<PlayerCondition>();
        if (collision.CompareTag("Player") && !isDestroyed)
        {
            
            if(player !=null)
            {
                ApplyEffect(player);
                MapManager.Instance.mapControllerTest.movingItmes.ReleaseObject(this.gameObject);
                //Destroy(gameObject, destroyDelay);
            }
            
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Time.time - lastTriggerTime < triggerCooldown) return; // ���� �ð� ���� ����
        lastTriggerTime = Time.time; // ������ ���� �ð� ������Ʈ

        PlayerCondition player = collision.GetComponent<PlayerCondition>();
        if (collision.CompareTag("Player") && !isDestroyed && player != null)
        {
            ApplyEffect(player);
            MapManager.Instance.mapControllerTest.movingItmes.ReleaseObject(this.gameObject);
        }
    }
}
