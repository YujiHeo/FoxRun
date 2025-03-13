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
    public GameObject GetRandomBuilding()
    {
        if (buildingPrefabs.Count == 0) return null;

        List<string> keys = new List<string>(buildingPrefabs.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];
        return buildingPrefabs[randomKey];
    }


}
