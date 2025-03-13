using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMove : MonoBehaviour
{
    public List<GameObject> startbuildingRight;
    public List<GameObject> startbuildingLeft;
    public GameObject buildingRight;
    public GameObject buildingLeft;
    public BuildingResource buildingResource;
    public MapElementData buildingObject;
    public float resetPositionZ = -10f;  // 도로가 이 위치까지 오면 맨 앞으로 이동
    public float startPositionZ = 10f;   // 도로를 맨 앞으로 배치할 위치

    private void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            startbuildingRight.Add(buildingResource.GetRandomBuilding());
            startbuildingLeft.Add(buildingResource.GetRandomBuilding());

        }

        for (int i = 0; i < startbuildingRight.Count; i++)
        {
            Vector3 spawnPositionRight = new Vector3(40, 0, i*20);
            Vector3 spawnPositionLeft = new Vector3(-40, 0, i * 20);
            startbuildingRight[i] = Instantiate(startbuildingRight[i], spawnPositionRight,Quaternion.Euler(0f,-90f,0));
            startbuildingRight[i].transform.SetParent(this.transform);
            startbuildingLeft[i] = Instantiate(startbuildingLeft[i], spawnPositionLeft, Quaternion.Euler(0f,90f,0f));
            startbuildingLeft[i].transform.SetParent(this.transform);
        }

    }


    private void Update()
    {
        for (int i = 0; i < startbuildingRight.Count; i++)
        {

            startbuildingRight[i].transform.Translate(Vector3.left * buildingObject.moveSpeed * Time.deltaTime);
            startbuildingLeft[i].transform.Translate(Vector3.right * buildingObject.moveSpeed * Time.deltaTime);

            // 특정 위치에 도달하면 맨 앞으로 이동
            if (startbuildingRight[i].transform.position.z <= resetPositionZ)
            {
                RepositionBuilding(startbuildingRight, buildingRight, i, -90f);
            }

            if (startbuildingLeft[i].transform.position.z <= resetPositionZ)
            {
                RepositionBuilding(startbuildingLeft, buildingLeft,i, 90f);
            }
        }

    }



    private void RepositionBuilding(List<GameObject> buildingList, GameObject newBuilding ,int index, float angle)
    {
        // 현재 이동할 건물
        GameObject oldBuilding = buildingList[index];

        GameObject lastBuilding = buildingList[buildingList.Count - 1];
        Vector3 newPosition = new Vector3(lastBuilding.transform.position.x, lastBuilding.transform.position.y, lastBuilding.transform.position.z + startPositionZ);

        newBuilding = Instantiate(buildingResource.GetRandomBuilding(), newPosition, Quaternion.Euler(0f, angle, 0f));
        newBuilding.transform.SetParent(this.transform);

        // 리스트 순서를 업데이트
        Destroy(oldBuilding);
        buildingList.RemoveAt(index);
        buildingList.Add(newBuilding);

    }

}
