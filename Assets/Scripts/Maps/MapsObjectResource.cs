using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;


public enum ResourceName
{
    Building,
    Vehicle,
    Item
}


public class MapsObjectResource : MonoBehaviour
{
    public string resourceFileName;
    private Dictionary<string, GameObject> ObjectPrefabs;
    Transform objectTranform; // ���� ������Ʈ ã���
    List<Transform> filteredObjects = new List<Transform>(); // Ư�� Ű���带 �����ϴ� �ڽ� ������Ʈ ����Ʈ
    Transform randomObject;

    private void Awake()
    {
        objectTranform = this.transform;
        LoadObjects();
        SaveObjects();
    }


    private void LoadObjects()
    {
        ObjectPrefabs = new Dictionary<string, GameObject>();

        // Resources �������� ���� ������ �� ���� �ε�
        GameObject[] loadedObjects = Resources.LoadAll<GameObject>(resourceFileName);

        foreach (GameObject building in loadedObjects)
        {
            ObjectPrefabs[building.name] = building;  // �̸����� Dictionary ����
        }

    }

    private void SaveObjects()
    {
        foreach (var kvp in ObjectPrefabs)
        {
            string ObjectName = kvp.Key;
            GameObject objectPrefab = kvp.Value;

            // ���� ������ �ν��Ͻ� ����
            GameObject newObject = Instantiate(objectPrefab);

            // �θ� BuildingResource�� ����
            newObject.transform.SetParent(this.transform);

            // �̸��� �����հ� �����ϰ� ����
            newObject.name = ObjectName;

            newObject.gameObject.SetActive(false);
        }
    }


    public GameObject GetRandomObjectFromChildren(string filterKeyword)
    {
        filteredObjects.Clear();

        foreach (Transform child in objectTranform)
        {
            if (child.name.Contains(filterKeyword))
            {
                filteredObjects.Add(child);
            }
        }

        if (filteredObjects.Count == 0)
        {
            LoadObjects();
            SaveObjects();

            foreach (Transform child in objectTranform)
            {
                if (child.name.Contains(filterKeyword))
                {
                    filteredObjects.Add(child);
                }
            }

        }

        // �������� �ϳ� ����
        randomObject = filteredObjects[Random.Range(0, filteredObjects.Count)];
        return randomObject.gameObject;
    }
}


