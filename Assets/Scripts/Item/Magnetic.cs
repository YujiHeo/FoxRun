using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{

    //�÷��̾ �ƴ϶� �������� �ν��ؼ� �������....

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
