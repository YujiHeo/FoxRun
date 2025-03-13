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

        // Resources �������� ���� ������ �� ���� �ε�
        GameObject[] loadedBuildings = Resources.LoadAll<GameObject>("Buildings");

        foreach (GameObject building in loadedBuildings)
        {
            buildingPrefabs[building.name] = building;  // �̸����� Dictionary ����
        }

    }

    // ���� ���� �������� �ż���
    public GameObject GetRandomBuilding()
    {
        if (buildingPrefabs.Count == 0) return null;

        List<string> keys = new List<string>(buildingPrefabs.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];
        return buildingPrefabs[randomKey];
    }


}
