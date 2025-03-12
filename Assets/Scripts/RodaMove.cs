using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float roadSpeed;
    private Vector3 moveVec;


    private void Update()
    {
        transform.Translate(transform.forward * roadSpeed * Time.deltaTime);
    }
}
