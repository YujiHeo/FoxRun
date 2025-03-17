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
    public GameObject GetRandomBuilding(string filterKeyword)
    {
        if (buildingPrefabs.Count == 0) return null;

        // Ư�� Ű���带 �����ϴ� �����ո� ����Ʈ�� �߰�
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
            Debug.LogWarning($"'{filterKeyword}'�� �����ϴ� �������� �����ϴ�!");
            return null;
        }

        string randomKey = filteredKeys[Random.Range(0, filteredKeys.Count)];
        return buildingPrefabs[randomKey];
    }


}
