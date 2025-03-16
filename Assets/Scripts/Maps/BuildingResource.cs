using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResource : MonoBehaviour
{
    private Dictionary<string, GameObject> buildingPrefabs;

    private void Awake()
    {
        LoadBuildings();
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


    // 랜던 빌딩 가져오는 매서드
    public GameObject GetRandomBuilding(string filterKeyword)
    {
        if (buildingPrefabs.Count == 0) return null;

        // 특정 키워드를 포함하는 프리팹만 리스트에 추가
        List<string> filteredKeys = new List<string>();

        foreach (var key in buildingPrefabs.Keys)
        {
            if (key.Contains(filterKeyword))
            {
                filteredKeys.Add(key);
            }
        }

        if (filteredKeys.Count == 0)
        {
            Debug.LogWarning($"'{filterKeyword}'를 포함하는 프리팹이 없습니다!");
            return null;
        }

        string randomKey = filteredKeys[Random.Range(0, filteredKeys.Count)];
        return buildingPrefabs[randomKey];
    }


}
