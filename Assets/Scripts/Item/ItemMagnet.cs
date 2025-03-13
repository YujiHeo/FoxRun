using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float magnetDistance = 30f;

    private Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(magnetDistance >= distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
