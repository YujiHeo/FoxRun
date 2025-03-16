using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{

    public float blinkDuration = 0.1f; // 깜빡이는 지속 시간
    public int blinkCount = 3; // 깜빡이는 횟수
    private Coroutine blinkCoroutine; // 깜빡이기 코루틴 저장
    private Color blinkColor = Color.red;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            Renderer playerRenderer = player.GetComponentInChildren<Renderer>();

            if(player.condition.isInvincibleTime)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),  // X축 랜덤 값
                    Random.Range(0.2f, 1f), // Y축은 너무 낮으면 안 떠오르므로 최소값 설정
                    Random.Range(-1f, 1f)   // Z축 랜덤 값
                ).normalized; // 정규화하여 크기를 1로 만듦

                float forcePower = 100f; // 힘의 크기

                rigidbody.AddForce(randomDirection * forcePower, ForceMode.Impulse);

                Invoke(nameof(ReleaseObstacle), 2f);
            }
            else
            {
                playerRenderer.material.color = blinkColor;
                player.condition.GetDamage(1);
            }

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            Renderer playerRenderer = player.GetComponentInChildren<Renderer>();
            playerRenderer.material.color = Color.white;


        }
    }

    private void ReleaseObstacle()
    {
        // 힘과 회전 속도 초기화
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        MapManager.Instance.mapControllerTest.movingObstacles.ReleaseObject(gameObject);
    }




    //private IEnumerator BlinkEffect(Player player)
    //{
    //    Renderer playerRenderer = player.GetComponentInChildren<Renderer>();
    //    if (playerRenderer == null) yield break;

    //    Color originalColor = playerRenderer.sharedMaterial.color;
    //    Color blinkColor = Color.red;

    //    for (int i = 0; i < blinkCount; i++)
    //    {
    //        playerRenderer.material.color = blinkColor;
    //        yield return new WaitForSeconds(blinkDuration / 2);

    //        playerRenderer.material.color = originalColor;
    //        yield return new WaitForSeconds(blinkDuration / 2);
    //    }

    //    playerRenderer.material.color = originalColor;
    //}



}
