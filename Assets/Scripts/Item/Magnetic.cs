using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{

    //플레이어가 아니라 아이템을 인식해서 끌어당기게....

    public float moveSpeed = 10f;
    public float magnetDistance = 30f;

    private GameObject[] items;


    void Start()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
    }

    void Update()
    {
        ApplyMagnet();
    }

    public void ApplyMagnet()
    {
        foreach (GameObject item in items)
        {
            if (PlayerCondition.isMagnet && item != null)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);

                if (distance <= magnetDistance)
                {
                    item.transform.position = Vector3.MoveTowards(item.transform.position, transform.position, moveSpeed * Time.deltaTime);
                }
            }
        }
    }
}
