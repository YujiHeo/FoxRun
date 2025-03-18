using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{

    public float blinkDuration = 0.1f; // �����̴� ���� �ð�
    public int blinkCount = 3; // �����̴� Ƚ��
    private Coroutine blinkCoroutine; // �����̱� �ڷ�ƾ ����
    private Color blinkColor = Color.red;
    private Rigidbody rigidbodyObstacle;

    private void Start()
    {
        rigidbodyObstacle = transform.GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            Renderer playerRenderer = player.GetComponentInChildren<Renderer>();


            if (player.condition.isInvincibleTime)
            {
                SoundManager.Instance.PlaySFX("DM-CGS-46", transform.position);

                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),  // X�� ���� ��
                    Random.Range(0.2f, 1f), // Y���� �ʹ� ������ �� �������Ƿ� �ּҰ� ����
                    Random.Range(1f, 2f)   // Z�� ���� ��
                ).normalized; // ����ȭ�Ͽ� ũ�⸦ 1�� ����

                float forcePower = 100f; // ���� ũ��

                rigidbodyObstacle.AddForce(randomDirection * forcePower, ForceMode.Impulse);

                Vector3 randomTorque = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                ) * 10f; // ȸ�� ���� ũ�� ����

                rigidbodyObstacle.AddTorque(randomTorque, ForceMode.Impulse);

                Invoke(nameof(ReleaseObstacle), 1f);
            }
            else
            {
                playerRenderer.material.color = blinkColor;
                player.condition.GetDamage(1);
                SoundManager.Instance.PlaySFX("DM-CGS-34", transform.position);
            }

        }
        else if (other.gameObject.CompareTag("Item"))
        {
            other.transform.position += Vector3.up * 5f;
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
        // ���� ȸ�� �ӵ� �ʱ�ȭ
        rigidbodyObstacle.velocity = Vector3.zero;
        rigidbodyObstacle.angularVelocity = Vector3.zero;
        rigidbodyObstacle.transform.rotation = Quaternion.identity;
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
