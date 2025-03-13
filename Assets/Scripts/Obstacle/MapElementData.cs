using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MapElement
{
    Road,
    Obstacle,
    Building
}

[CreateAssetMenu(fileName = "MapData", menuName = "MapsData")]
public class MapElementData : ScriptableObject
{
    public float moveSpeed = 10f;
    public MapElement mapElement;
}
