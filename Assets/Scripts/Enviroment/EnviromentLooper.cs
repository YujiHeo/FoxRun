using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentLooper : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnviromentLoop"))
        {
            Debug.Log(1);
            other.GetComponentInParent<EnviromentSetting>().RePositionEnviroment(other.transform.parent); 

        }
    }
}
