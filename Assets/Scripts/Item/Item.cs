using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float destroyDelay = 0.1f; //충돌했을 시 아이템 오브젝트가 사라질 때까지의 지연시간
    private bool isDestroyed = false;
    private float lastTriggerTime = 0f;  // 마지막으로 실행된 시간
    private float triggerCooldown = 0.2f;  // 실행 간격 (0.2초마다 실행)

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
        if (Time.time - lastTriggerTime < triggerCooldown) return; // 일정 시간 간격 유지
        lastTriggerTime = Time.time; // 마지막 실행 시간 업데이트

        PlayerCondition player = collision.GetComponent<PlayerCondition>();
        if (collision.CompareTag("Player") && !isDestroyed && player != null)
        {
            ApplyEffect(player);
            MapManager.Instance.mapControllerTest.movingItmes.ReleaseObject(this.gameObject);
        }
    }
}
