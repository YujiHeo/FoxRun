using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnviromentSetting : MonoBehaviour
{
    public List<Transform> snowEnviroment;
    public float moveSpeed;
    public Vector3 lastPosition;
    void Start()
    {
        //lastPosition = snowEnviroment.OrderByDescending(obj => obj.transform.position.z).First().transform.position;

        Transform highestZObject = null;
        float maxZ = float.MinValue;

        foreach (var obj in snowEnviroment)
        {
            if (obj.transform.position.z > maxZ)
            {
                maxZ = obj.transform.position.z;
                highestZObject = obj;
            }
        }

        if (highestZObject != null)
        {
            lastPosition = highestZObject.transform.localPosition;
        }
    }

    void Update()
    {
        for (int i = 0; i < snowEnviroment.Count; i++)
        {
            snowEnviroment[i].transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }

    public void RePositionEnviroment(Transform hitTransform)
    {
        hitTransform.localPosition = lastPosition;
    }
}
