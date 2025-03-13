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
    public float resetPositionZ = -10f;  // ���ΰ� �� ��ġ���� ���� �� ������ �̵�
    public float startPositionZ = 10f;   // ���θ� �� ������ ��ġ�� ��ġ

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

            // Ư�� ��ġ�� �����ϸ� �� ������ �̵�
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
        // ���� �̵��� �ǹ�
        GameObject oldBuilding = buildingList[index];

        GameObject lastBuilding = buildingList[buildingList.Count - 1];
        Vector3 newPosition = new Vector3(lastBuilding.transform.position.x, lastBuilding.transform.position.y, lastBuilding.transform.position.z + startPositionZ);

        newBuilding = Instantiate(buildingResource.GetRandomBuilding(), newPosition, Quaternion.Euler(0f, angle, 0f));
        newBuilding.transform.SetParent(this.transform);

        // ����Ʈ ������ ������Ʈ
        Destroy(oldBuilding);
        buildingList.RemoveAt(index);
        buildingList.Add(newBuilding);

    }

}
