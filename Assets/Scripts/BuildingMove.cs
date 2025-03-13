using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMove : MonoBehaviour
{
    public List<GameObject> buildingRight;
    public List<GameObject> buildingLeft;
    public BuildingResource buildingResource;
    public MapElementData buildingObject;
    public float resetPositionZ = -10f;  // 도로가 이 위치까지 오면 맨 앞으로 이동
    public float startPositionZ = 10f;   // 도로를 맨 앞으로 배치할 위치

    private void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            buildingRight.Add(buildingResource.GetRandomBuilding());
            buildingLeft.Add(buildingResource.GetRandomBuilding());

        }

        for (int i = 0; i < buildingRight.Count; i++)
        {
            Vector3 spawnPositionRight = new Vector3(40, 0, i*20);
            Vector3 spawnPositionLeft = new Vector3(-40, 0, i * 20);
            buildingRight[i] = Instantiate(buildingRight[i], spawnPositionRight,Quaternion.Euler(0f,-90f,0));
            buildingLeft[i] = Instantiate(buildingLeft[i], spawnPositionLeft, Quaternion.Euler(0f,90f,0f));
        }

    }


    private void Update()
    {
        for (int i = 0; i < buildingRight.Count; i++)
        {

            buildingRight[i].transform.Translate(Vector3.left * buildingObject.moveSpeed * Time.deltaTime);
            buildingLeft[i].transform.Translate(Vector3.right * buildingObject.moveSpeed * Time.deltaTime);

            // 특정 위치에 도달하면 맨 앞으로 이동
            if (buildingRight[i].transform.position.z <= resetPositionZ)
            {
                RepositionBuilding(buildingRight, i,-90f);

            }

            if (buildingLeft[i].transform.position.z <= resetPositionZ)
            {
                RepositionBuilding(buildingLeft, i, 90f);
            }
        }

    }



    private void RepositionBuilding(List<GameObject> buildingList, int index, float angle)
    {
        // 현재 이동할 건물
        GameObject oldBuilding = buildingList[index];

        // 리스트에서 가장 마지막에 있는 오브젝트를 기준으로 새로운 위치 설정
        Transform lastRoad = buildingList[buildingList.Count - 1].transform;
        oldBuilding.transform.position = new Vector3(lastRoad.position.x, lastRoad.position.y, lastRoad.position.z + startPositionZ);

        // 리스트 순서를 업데이트
        buildingList.RemoveAt(index);
        buildingList.Add(oldBuilding);

    }



    //private void RepositionRoad(List<Transform> buildingList, int index)
    //{
    //    // 현재 이동할 건물
    //    Transform oldBuilding = buildingList[index];

    //    // 랜덤한 새로운 건물 가져오기 (Dictionary 사용)
    //    GameObject newBuildingPrefab = buildingResource.GetRandomBuilding();
    //    if (newBuildingPrefab == null)
    //    {
    //        Debug.LogWarning("BuildingResource에서 랜덤 건물을 가져오지 못했습니다.");
    //        return;
    //    }

    //    // 새로운 건물을 기존 위치에 배치
    //    Transform lastBuilding = buildingList[buildingList.Count - 1];
    //    Transform newBuilding = Instantiate(newBuildingPrefab, oldBuilding.position, Quaternion.identity).transform;
    //    newBuilding.position = new Vector3(lastBuilding.position.x, lastBuilding.position.y, lastBuilding.position.z + startPositionZ);


    //    // 리스트 업데이트
    //    Destroy(oldBuilding.gameObject); // 기존 건물 제거
    //    buildingList[index] = newBuilding;
    //}
}
