using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHit : MonoBehaviour
{
    public float checkRate = 5f;
    private float lastCheckTime;

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Player>(out Player playerCondition))
        {
            if (Time.time - lastCheckTime > checkRate)
            {
                lastCheckTime = Time.time;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player playerCondition))
        {
            if (Time.time - lastCheckTime >= checkRate)
            {
                lastCheckTime = Time.time; // 다음 체크 시간 갱신
                Renderer playerRendere = playerCondition.GetComponentInChildren<Renderer>();
                playerRendere.material.color = Color.red;
                playerCondition.condition.GetDamage(1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player playerCondition))
        {
            lastCheckTime = 0f;
            Renderer playerRendere = playerCondition.GetComponentInChildren<Renderer>();
            playerRendere.material.color = Color.white;

        }
    }
}

