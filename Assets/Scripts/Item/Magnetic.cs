using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{

    //플레이어가 아니라 아이템을 인식해서 끌어당기게....
    
    public float moveSpeed = 10f;
    public float magnetDistance = 30f;

    private Transform item;


    void Start()
    {
        item = GameObject.FindGameObjectWithTag("Item").transform;
    }

    void Update()
    {
        ApplyMagnet();
    }

    public void ApplyMagnet()
    {
        if (PlayerCondition.isMagnet == true)
        {
            float distance = Vector3.Distance(transform.position, item.position);

            if (magnetDistance >= distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, item.position, moveSpeed * Time.deltaTime);
            }
        }

        else
        {
            return;
        }
    }
}
