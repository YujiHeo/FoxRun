using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoveRevision : MonoBehaviour
{
    public List<GameObject> startbuildingRight;
    public List<GameObject> startbuildingLeft;
    public GameObject buildingRight; // 새로운 오른쪽 오브젝트
    public GameObject buildingLeft; // 새로운 왼쪽 오브젝트
    public MapElementData buildingData;
    public BuildingResourceRevision resourceRevision;
    public float sidePositionX;
    public int buildingCount;
    public float resetPositionZ = -10f;  // 도로가 이 위치까지 오면 맨 앞으로 이동
    public float startPositionZ = 10f;   // 도로를 맨 앞으로 배치할 위치



    private void Start()
    {
        for (int i = 0; i < buildingCount; i++)
        {
            startbuildingRight.Add(resourceRevision.GetRandomBuildingFromChildren("Building"));
            startbuildingRight[i].transform.SetParent(this.transform);
            startbuildingLeft.Add(resourceRevision.GetRandomBuildingFromChildren("Building"));
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

        for (int i = 0; i < startbuildingRight.Count; i++)
        {

            startbuildingRight[i].transform.Translate(Vector3.left * buildingData.moveSpeed * Time.deltaTime);
            startbuildingLeft[i].transform.Translate(Vector3.right * buildingData.moveSpeed * Time.deltaTime);


            // 특정 위치에 도달하면 맨 앞으로 이동
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



    private void RepositionBuilding(List<GameObject> _buildingList, GameObject _newBuilding, int _index, float _angle)
    {
        // 현재 앞에 위치한 건물
        GameObject oldBuilding = _buildingList[_index];
        
        // 뒤쪽 건물 위치
        GameObject lastBuilding = _buildingList[_buildingList.Count - 1];
        Vector3 newPosition = new Vector3(lastBuilding.transform.position.x, lastBuilding.transform.position.y, lastBuilding.transform.position.z + startPositionZ);

        _newBuilding = InstantiateBuilding("Building");
        _newBuilding.transform.position = newPosition;
        _newBuilding.transform.rotation = Quaternion.Euler(0f, _angle, 0f);
        _newBuilding.transform.SetParent(this.transform);
        _newBuilding.gameObject.SetActive(true);

        // 리스트 순서를 업데이트
        oldBuilding.gameObject.SetActive(false);
        oldBuilding.transform.SetParent(resourceRevision.transform);
        _buildingList.RemoveAt(_index);
        _buildingList.Add(_newBuilding);

    }

    private GameObject InstantiateBuilding(string _name)
    {    
        return resourceRevision.GetRandomBuildingFromChildren(_name);
    }
}
