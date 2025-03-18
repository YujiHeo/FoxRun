using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;


public enum ResourceName
{
    Spring,
    Summer,
    Fall,
    Winter,
    Building,
    Vehicle,
    Item,
    Props
}


public class MapsObjectResource : MonoBehaviour
{
    public ResourceName resourceFileName;
    private Dictionary<string, GameObject> ObjectPrefabs;
    Transform objectTranform;
    List<Transform> filteredObjects = new List<Transform>();
    Transform randomObject;

    private void Awake()
    {
        objectTranform = this.transform;
        LoadObjects(resourceFileName.ToString());
        SaveObjects();
    }


    private void LoadObjects(string _resourcFilName)
    {
        ObjectPrefabs = new Dictionary<string, GameObject>();

        // Resources �������� ���� ������ �� ���� �ε�
        GameObject[] loadedObjects = Resources.LoadAll<GameObject>(_resourcFilName);

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


    public GameObject GetRandomObjectFromChildren(string filterKeyword) // ������Ʈ �����ϸ� �߰����ִ� �ż���
    {
        filteredObjects.Clear(); // ����Ʈ �ʱ�ȭ

        foreach (Transform child in objectTranform)
        {
            if (child.name.Contains(filterKeyword))
            {
                filteredObjects.Add(child);
            }
        } // ������ ������Ʈ Ű���忡 �ش��� ������Ʈ �ִ��� Ȯ��

        if (filteredObjects.Count == 0)
        {
            if(!ObjectPrefabs.Keys.Any(key => key.Contains(filterKeyword)))
            {
               LoadObjects(filterKeyword);
            }

            SaveObjects();

            foreach (Transform child in objectTranform)
            {
                if (child.name.Contains(filterKeyword))
                {
                    filteredObjects.Add(child);
                }
            }

        } // ������ ���� Resource �������� ��������

        // �������� �ϳ� ����
        randomObject = filteredObjects[Random.Range(0, filteredObjects.Count)];
        return randomObject.gameObject;
    }
}


