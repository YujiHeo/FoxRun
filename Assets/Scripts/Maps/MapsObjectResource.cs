using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using TreeEditor;
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

        // Resources 폴더에서 빌딩 프리팹 한 번에 로드
        GameObject[] loadedObjects = Resources.LoadAll<GameObject>(_resourcFilName);

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


    public GameObject GetRandomObjectFromChildren(string filterKeyword) // 오브젝트 부족하면 추가해주는 매서드
    {
        filteredObjects.Clear(); // 리스트 초기화

        foreach (Transform child in objectTranform)
        {
            if (child.name.Contains(filterKeyword))
            {
                filteredObjects.Add(child);
            }
        } // 하위에 오브젝트 키워드에 해당한 오브젝트 있는지 확인

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

        } // 없으면 새로 Resource 폴더에서 가져오기

        // 랜덤으로 하나 선택
        randomObject = filteredObjects[Random.Range(0, filteredObjects.Count)];
        return randomObject.gameObject;
    }
}


