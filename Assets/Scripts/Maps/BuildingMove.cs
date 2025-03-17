using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingMove : MonoBehaviour
{
    public List<GameObject> startbuildingRight;
    public List<GameObject> startbuildingLeft;
    public GameObject buildingRight;
    public GameObject buildingLeft;
    public MapElementData buildingData;
    public BuildingResource buildingResource;
    public float sidePositionX;
    public int buildingCount;
    public float resetPositionZ = -10f;  // ���ΰ� �� ��ġ���� ���� �� ������ �̵�
    public float startPositionZ = 10f;   // ���θ� �� ������ ��ġ�� ��ġ



    private void Start()
    {
        for (int i = 0; i < buildingCount; i++)
        {
            startbuildingRight.Add(buildingResource.GetRandomBuilding("Building"));
            startbuildingLeft.Add(buildingResource.GetRandomBuilding("Building"));

        }

        for (int i = 0; i < startbuildingRight.Count; i++)
        {
            Vector3 spawnPositionRight = new Vector3(sidePositionX, 0, i * startPositionZ);
            Vector3 spawnPositionLeft = new Vector3(-sidePositionX, 0, i * startPositionZ);
            startbuildingRight[i] = Instantiate(startbuildingRight[i], spawnPositionRight, Quaternion.Euler(0f, -90f, 0));
            startbuildingRight[i].transform.SetParent(this.transform);
            startbuildingLeft[i] = Instantiate(startbuildingLeft[i], spawnPositionLeft, Quaternion.Euler(0f, 90f, 0f));
            startbuildingLeft[i].transform.SetParent(this.transform);
        }

    }


    private void Update()
    {

        for (int i = 0; i < startbuildingRight.Count; i++)
        {

            startbuildingRight[i].transform.Translate(Vector3.left * buildingData.moveSpeed * Time.deltaTime);
            startbuildingLeft[i].transform.Translate(Vector3.right * buildingData.moveSpeed * Time.deltaTime);

            // Ư�� ��ġ�� �����ϸ� �� ������ �̵�
            if (startbuildingRight[i].transform.position.z <= resetPositionZ)
            {
                RepositionBuilding(startbuildingRight, buildingRight, i, -90f);
            }

            if (startbuildingLeft[i].transform.position.z <= resetPositionZ)
            {
                RepositionBuilding(startbuildingLeft, buildingLeft, i, 90f);
            }
        }

    }



    private void RepositionBuilding(List<GameObject> buildingList, GameObject newBuilding, int index, float angle)
    {
        // ���� �̵��� �ǹ�
        GameObject oldBuilding = buildingList[index];

        GameObject lastBuilding = buildingList[buildingList.Count - 1];
        Vector3 newPosition = new Vector3(lastBuilding.transform.position.x, lastBuilding.transform.position.y, lastBuilding.transform.position.z + startPositionZ);

        if (buildingData.moveSpeed < 40f)
        {
            newBuilding = InstantiateBuilding(newPosition, angle, "Building");
        }
        else if (buildingData.moveSpeed < 60f)
        {
            newBuilding = InstantiateBuilding(newPosition, angle, "Sky");
        }
        else
        {
            newBuilding = InstantiateBuilding(newPosition, angle, "Building");
        }

        newBuilding.transform.SetParent(this.transform);

        // ����Ʈ ������ ������Ʈ
        Destroy(oldBuilding);
        buildingList.RemoveAt(index);
        buildingList.Add(newBuilding);

    }

    private GameObject InstantiateBuilding(Vector3 _newPosition, float _angle, string _name)
    {
        return Instantiate(buildingResource.GetRandomBuilding(_name), _newPosition, Quaternion.Euler(0f, _angle, 0f));
    }

}
