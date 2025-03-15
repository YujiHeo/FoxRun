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
    Transform objectTranform; // 하위 오브젝트 찾기용
    List<Transform> filteredObjects = new List<Transform>(); // 특정 키워드를 포함하는 자식 오브젝트 리스트
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

        // Resources 폴더에서 빌딩 프리팹 한 번에 로드
        GameObject[] loadedObjects = Resources.LoadAll<GameObject>(resourceFileName);

        foreach (GameObject building in loadedObjects)
        {
            ObjectPrefabs[building.name] = building;  // 이름으로 Dictionary 저장
        }

    }

    private void SaveObjects()
    {
        foreach (var kvp in ObjectPrefabs)
        {
            string ObjectName = kvp.Key;
            GameObject objectPrefab = kvp.Value;

            // 빌딩 프리팹 인스턴스 생성
            GameObject newObject = Instantiate(objectPrefab);

            // 부모를 BuildingResource로 설정
            newObject.transform.SetParent(this.transform);

            // 이름을 프리팹과 동일하게 설정
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

        // 랜덤으로 하나 선택
        randomObject = filteredObjects[Random.Range(0, filteredObjects.Count)];
        return randomObject.gameObject;
    }
}


