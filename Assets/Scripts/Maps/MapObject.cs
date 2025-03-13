using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public MapElementData elementData;

    private void Update()
    {
        this.transform.Translate(Vector3.back * elementData.moveSpeed * Time.deltaTime);
    }


}
