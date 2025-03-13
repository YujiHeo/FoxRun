using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float moveSpeed;

    private void Start()
    {
    }

    private void Update()
    {
        this.transform.Translate(Vector3.back*moveSpeed*Time.deltaTime);
    }



}
