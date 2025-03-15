using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{

    public float blinkDuration = 0.1f; // ±ôºýÀÌ´Â Áö¼Ó ½Ã°£
    public int blinkCount = 3; // ±ôºýÀÌ´Â È½¼ö
    private Coroutine blinkCoroutine; // ±ôºýÀÌ±â ÄÚ·çÆ¾ ÀúÀå
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
            playerRenderer.material.color = blinkColor;
            //MapManager.Instance.mapControllerTest.movingObstacles.ReleaseObject(gameObject);
            player.condition.GetDamage(1);
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
