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
    public float resetPositionZ = -10f;  // ���ΰ� �� ��ġ���� ���� �� ������ �̵�
    public float startPositionZ = 10f;   // ���θ� �� ������ ��ġ�� ��ġ

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

            // Ư�� ��ġ�� �����ϸ� �� ������ �̵�
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
        // ���� �̵��� �ǹ�
        GameObject oldBuilding = buildingList[index];

        // ����Ʈ���� ���� �������� �ִ� ������Ʈ�� �������� ���ο� ��ġ ����
        Transform lastRoad = buildingList[buildingList.Count - 1].transform;
        oldBuilding.transform.position = new Vector3(lastRoad.position.x, lastRoad.position.y, lastRoad.position.z + startPositionZ);

        // ����Ʈ ������ ������Ʈ
        buildingList.RemoveAt(index);
        buildingList.Add(oldBuilding);

    }



    //private void RepositionRoad(List<Transform> buildingList, int index)
    //{
    //    // ���� �̵��� �ǹ�
    //    Transform oldBuilding = buildingList[index];

    //    // ������ ���ο� �ǹ� �������� (Dictionary ���)
    //    GameObject newBuildingPrefab = buildingResource.GetRandomBuilding();
    //    if (newBuildingPrefab == null)
    //    {
    //        Debug.LogWarning("BuildingResource���� ���� �ǹ��� �������� ���߽��ϴ�.");
    //        return;
    //    }

    //    // ���ο� �ǹ��� ���� ��ġ�� ��ġ
    //    Transform lastBuilding = buildingList[buildingList.Count - 1];
    //    Transform newBuilding = Instantiate(newBuildingPrefab, oldBuilding.position, Quaternion.identity).transform;
    //    newBuilding.position = new Vector3(lastBuilding.position.x, lastBuilding.position.y, lastBuilding.position.z + startPositionZ);


    //    // ����Ʈ ������Ʈ
    //    Destroy(oldBuilding.gameObject); // ���� �ǹ� ����
    //    buildingList[index] = newBuilding;
    //}
}
