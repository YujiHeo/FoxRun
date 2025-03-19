using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticRevision : MonoBehaviour
{
    //플레이어가 아니라 아이템을 인식해서 끌어당기게....

    public float moveSpeed = 10f;
    public float magnetDistance = 30f;
    public GameObject items;

    private List<GameObject> itemsList = new List<GameObject>();
    private GameObject child;
    private int childCount;


    void Update()
    {
        itemsList.Clear();

        childCount = items.transform.childCount;

        foreach (Transform childTransform in items.transform)
        {
            if (childTransform.gameObject.activeSelf)
            {
                itemsList.Add(childTransform.gameObject);
            }
        }

        ApplyMagnet();
    }

    public void ApplyMagnet()
    {
        foreach (GameObject item in itemsList)
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
