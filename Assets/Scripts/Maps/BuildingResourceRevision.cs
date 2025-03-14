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

        // Resources �������� ���� ������ �� ���� �ε�
        GameObject[] loadedBuildings = Resources.LoadAll<GameObject>("Buildings");

        foreach (GameObject building in loadedBuildings)
        {
            buildingPrefabs[building.name] = building;  // �̸����� Dictionary ����
        }

    }

    private void SaveBuildings()
    {
        foreach (var kvp in buildingPrefabs)
        {
            string buildingName = kvp.Key;
            GameObject buildingPrefab = kvp.Value;

            // ���� ������ �ν��Ͻ� ����
            GameObject newBuilding = Instantiate(buildingPrefab);

            // �θ� BuildingResource�� ����
            newBuilding.transform.SetParent(this.transform);

            // �̸��� �����հ� �����ϰ� ����
            newBuilding.name = buildingName;

            newBuilding.gameObject.SetActive(false);
        }
    }


    // ���� ���� �������� �ż���
    //public GameObject GetRandomBuilding(string filterKeyword)
    //{
    //    if (buildingPrefabs.Count == 0) return null;

    //    // Ư�� Ű���带 �����ϴ� �����ո� ����Ʈ�� �߰�
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
    //        Debug.LogWarning($"'{filterKeyword}'�� �����ϴ� �������� �����ϴ�!");
    //        return null;
    //    }

    //    string randomKey = filteredKeys[Random.Range(0, filteredKeys.Count)];
    //    return buildingPrefabs[randomKey];
    //}

    public GameObject GetRandomBuildingFromChildren(string filterKeyword)
    {
        Transform buildingResource = transform; // ���� BuildingResource ������Ʈ�� Transform

        if (buildingResource.childCount == 0) return null;

        // Ư�� Ű���带 �����ϴ� �ڽ� ������Ʈ ����Ʈ
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
            Debug.LogWarning($"'{filterKeyword}'�� �����ϴ� ������ �����ϴ�!");
            return null;
        }

        // �������� �ϳ� ����
        Transform randomBuilding = filteredBuildings[Random.Range(0, filteredBuildings.Count)];
        return randomBuilding.gameObject;
    }



}
