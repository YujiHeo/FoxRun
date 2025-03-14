using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourceRevision : MonoBehaviour
{
    private Dictionary<string, GameObject> buildingPrefabs;

    private void Awake()
    {
        LoadBuildings();
        SaveBuildings();
    }


    private void LoadBuildings()
    {
        buildingPrefabs = new Dictionary<string, GameObject>();

        // Resources 폴더에서 빌딩 프리팹 한 번에 로드
        GameObject[] loadedBuildings = Resources.LoadAll<GameObject>("Buildings");

        foreach (GameObject building in loadedBuildings)
        {
            buildingPrefabs[building.name] = building;  // 이름으로 Dictionary 저장
        }

    }

    private void SaveBuildings()
    {
        foreach (var kvp in buildingPrefabs)
        {
            string buildingName = kvp.Key;
            GameObject buildingPrefab = kvp.Value;

            // 빌딩 프리팹 인스턴스 생성
            GameObject newBuilding = Instantiate(buildingPrefab);

            // 부모를 BuildingResource로 설정
            newBuilding.transform.SetParent(this.transform);

            // 이름을 프리팹과 동일하게 설정
            newBuilding.name = buildingName;

            newBuilding.gameObject.SetActive(false);
        }
    }


    // 랜던 빌딩 가져오는 매서드
    //public GameObject GetRandomBuilding(string filterKeyword)
    //{
    //    if (buildingPrefabs.Count == 0) return null;

    //    // 특정 키워드를 포함하는 프리팹만 리스트에 추가
    //    List<string> filteredKeys = new List<string>();

    //    foreach (var key in buildingPrefabs.Keys)
    //    {
    //        if (key.Contains(filterKeyword))  
    //        {
    //            filteredKeys.Add(key);
    //        }
    //    }

    //    if (filteredKeys.Count == 0)
    //    {
    //        Debug.LogWarning($"'{filterKeyword}'를 포함하는 프리팹이 없습니다!");
    //        return null;
    //    }

    //    string randomKey = filteredKeys[Random.Range(0, filteredKeys.Count)];
    //    return buildingPrefabs[randomKey];
    //}

    public GameObject GetRandomBuildingFromChildren(string filterKeyword)
    {
        Transform buildingResource = transform; // 현재 BuildingResource 오브젝트의 Transform

        if (buildingResource.childCount == 0) return null;

        // 특정 키워드를 포함하는 자식 오브젝트 리스트
        List<Transform> filteredBuildings = new List<Transform>();

        foreach (Transform child in buildingResource)
        {
            if (child.name.Contains(filterKeyword))
            {
                filteredBuildings.Add(child);
            }
        }

        if (filteredBuildings.Count == 0)
        {
            Debug.LogWarning($"'{filterKeyword}'를 포함하는 빌딩이 없습니다!");
            return null;
        }

        // 랜덤으로 하나 선택
        Transform randomBuilding = filteredBuildings[Random.Range(0, filteredBuildings.Count)];
        return randomBuilding.gameObject;
    }



}
