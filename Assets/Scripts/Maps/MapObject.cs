using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public MapElementData elementData;

    private void Update()
    {
        switch(elementData.mapElementType)
        {
            case MapElementType.Obstacle:
                this.transform.Translate(Vector3.back * elementData.moveSpeed * Time.deltaTime);
                break;
            case MapElementType.Building:
                break;
        }
    }


}
