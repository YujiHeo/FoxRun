using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingMoveRevision : MonoBehaviour
{
    public List<GameObject> startbuildingRight;
    public List<GameObject> startbuildingLeft;
    public GameObject buildingRight; // ���ο� ������ ������Ʈ
    public GameObject buildingLeft; // ���ο� ���� ������Ʈ
    public MapElementData buildingData;
    public MapsObjectResource resourceRevision;
    public float sidePositionX;
    public int buildingCount;
    public float resetPositionZ = -10f;  // ���ΰ� �� ��ġ���� ���� �� ������ �̵�
    public float startPositionZ = 10f;   // ���θ� �� ������ ��ġ�� ��ġ
    public ResourceName resourceName;

    private List<GameObject> inactiveBuilding = new List<GameObject>(); // Ȱ��ȭ�� ���� ����Ʈ



    private void Start()
    {
        for (int i = 0; i < buildingCount; i++)
        {
            startbuildingRight.Add(resourceRevision.GetRandomObjectFromChildren(resourceName.ToString()));
            startbuildingRight[i].transform.SetParent(this.transform);
            startbuildingLeft.Add(resourceRevision.GetRandomObjectFromChildren(resourceName.ToString()));
            startbuildingLeft[i].transform.SetParent(this.transform);

        }

        for (int i = 0; i < startbuildingRight.Count; i++)
        {
            Vector3 spawnPositionRight = new Vector3(sidePositionX, 0, i * startPositionZ);
            Vector3 spawnPositionLeft = new Vector3(-sidePositionX, 0, i * startPositionZ);

            startbuildingRight[i].transform.position = spawnPositionRight;
            startbuildingRight[i].transform.rotation = Quaternion.Euler(0f,-90f,0f);
            startbuildingRight[i].gameObject.SetActive(true);


            startbuildingLeft[i].transform.position = spawnPositionLeft;
            startbuildingLeft[i].transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            startbuildingLeft[i].gameObject.SetActive(true);

        }

    }


    private void Update()
    {
        if(buildingData.moveSpeed > 0f)
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


    }



    private void RepositionBuilding(List<GameObject> _buildingList, GameObject _newBuilding, int _index, float _angle)
    {
        inactiveBuilding.Clear();
        // ���� �տ� ��ġ�� �ǹ�
        GameObject oldBuilding = _buildingList[_index];
        
        // ���� �ǹ� ��ġ
        GameObject lastBuilding = _buildingList[_buildingList.Count - 1];
        Vector3 newPosition = new Vector3(lastBuilding.transform.position.x, lastBuilding.transform.position.y, lastBuilding.transform.position.z + startPositionZ);


        // ��Ȱ��ȭ�� ���� ����Ʈ ã�ƿ���
        foreach (Transform ob in this.transform)
        {
            if (!ob.gameObject.activeSelf && ob.name.Contains(resourceName.ToString()))
            {
                inactiveBuilding.Add(ob.gameObject);
            }
        }

        if(inactiveBuilding.Count == 0)
        {
            for(int i = 0; i < buildingCount; i++)
            {
                GameObject addBuilding = InstantiateBuilding(resourceName.ToString());
                addBuilding.transform.SetParent(this.transform);

                inactiveBuilding.Add(addBuilding);
            }
        }


        _newBuilding = inactiveBuilding[Random.Range(0, inactiveBuilding.Count)];
        _newBuilding.transform.position = newPosition;
        _newBuilding.transform.rotation = Quaternion.Euler(0f, _angle, 0f);
        _newBuilding.gameObject.SetActive(true);

        // ����Ʈ ������ ������Ʈ
        oldBuilding.gameObject.SetActive(false);
        _buildingList.RemoveAt(_index);
        _buildingList.Add(_newBuilding);

    }

    private GameObject InstantiateBuilding(string _name)
    {    
        return resourceRevision.GetRandomObjectFromChildren(_name);
    }
}
