using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class MapsMovingObstacles : MonoBehaviour
{

    [HideInInspector] public List<GameObject> movingObjects;
    public int movingObjetctsCount = 10;
    public MapsObjectResource objectResource;
    public List<float> spawnX;
    public float spawnZ = 120f;
    public float resetZ = -10f;
    public float ItemspawnY = 1f;
    public float objectsSpawnY = 0;
    public float minSpawnInterval = 0.3f; // 최소 생성 간격 (너무 빠르지 않도록 제한)
    public float speedFactor = 0.05f;

    public ResourceName resourceName;

    private float spawnInterval; // 기본 생성 간격
    private GameObject newObject; // 장애물저장고에서 새로 담을 게임오브젝트 변수
    private List<GameObject> inactiveObjects = new List<GameObject>(); // 활성화된 장애물 확인 용도
    private GameObject temptObject; // 빈 게임오브젝트


    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < movingObjetctsCount; i++)
        {
             temptObject = objectResource.GetRandomObjectFromChildren(resourceName.ToString());
             temptObject.transform.SetParent(this.transform);
            //movingObjects[i].transform.SetParent(this.transform);         
        }

        StartCoroutine(SpawnObstacleRoutine());
    }


    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            SpawnOb();

            for (int i = 0; i < movingObjects.Count; i++)
            {
                if (movingObjects[i].transform.position.z <= resetZ) // 리셋 위치 확인
                {
                    movingObjects[i].SetActive(false);
                    movingObjects.RemoveAt(i);
                }
            }

            float adjustedInterval = Mathf.Max(minSpawnInterval, spawnInterval - speedFactor); // 최소값 제한
            yield return new WaitForSeconds(adjustedInterval); // 동적으로 생성 주기 변경
        }
    }


    private void SpawnOb()
    {
        if (spawnX == null || spawnX.Count == 0)
        {
            Debug.LogError("spawnX 배열이 비어 있습니다! 인스펙터에서 값 설정이 필요합니다.");
            return;
        }

         RandomObstacle(objectsSpawnY, resourceName.ToString());
    }


    private void RandomObstacle(float _sapwY, string _typeName)
    {
        inactiveObjects.Clear();

        foreach (Transform ob in this.transform)
        {
            if (!ob.gameObject.activeSelf)
            {
                inactiveObjects.Add(ob.gameObject);
            }
        }


        if (inactiveObjects.Count == 0)
        {
            for (int i = 0; i < movingObjetctsCount; i++)
            {
                GameObject _newObjects = objectResource.GetRandomObjectFromChildren(_typeName);
                _newObjects.transform.SetParent(this.transform); // 부모를 설정하여 obstacle 하위로 이동

                inactiveObjects.Add(_newObjects);
            }
        }

        int index = Random.Range(0, inactiveObjects.Count); // 장애물 리스트에서 랜덤으로 인덱스 선택

        int indexPosition = Random.Range(0, spawnX.Count); // 3가지 길 중 랜덤 인덱스 선택
        float _spawnX = spawnX[indexPosition]; // 선택된 랜덤 위치 선정

        Vector3 spawnPosition = new Vector3(_spawnX, _sapwY, spawnZ); // 최종 위치 결정

        GameObject newObstacle = inactiveObjects[index];
        newObstacle.transform.position = spawnPosition;
        newObstacle.gameObject.SetActive(true);
        movingObjects.Add(newObstacle);

    }

    public void ReleaseObject(GameObject _object)
    {
        if (movingObjects.Contains(_object)) // 리스트에 있는지 확인
        {
            _object.transform.position = Vector3.zero;
            movingObjects.Remove(_object); // 리스트에서 제거
            _object.SetActive(false); // 비활성화 처리
        }
        else
        {
            Debug.LogWarning($"'{_object.name}'은(는) 리스트에 존재하지 않는 오브젝트입니다!");
        }
    }


}
