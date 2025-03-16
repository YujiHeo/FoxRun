using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public float gap;
    void Start()
    {
        this.transform.position = new Vector3(0,0, gap);
    }


}
