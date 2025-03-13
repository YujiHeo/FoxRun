using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
    public MapMove mapmove;
    public BuildingMove buildingMove;
    public MapElementData obstalceSpeed;
    public SpawnObstacle spawnObstacle;
    public float moveSpeed;

    private void Awake()
    {
        mapmove = GetComponent<MapMove>();
    }

    private void Start()
    {
        mapmove.roadData.moveSpeed = moveSpeed;
        obstalceSpeed.moveSpeed = moveSpeed;
        buildingMove.buildingObject.moveSpeed = moveSpeed;
        
    }
}
