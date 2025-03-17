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
        if (this.gameObject.activeInHierarchy) this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        else this.transform.position = new Vector3(0f,0f, 120f);
    }



}
