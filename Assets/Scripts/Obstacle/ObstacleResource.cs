using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleResource : MonoBehaviour
{
    private Dictionary<string, GameObject> obstalceResource;
    Transform obstacleResource; // 현재 ObstacleResource 오브젝트의 Transform
    List<Transform> filteredObstacles = new List<Transform>(); // 특정 키워드를 포함하는 자식 오브젝트 리스트
    Transform randomObstacle;

    private void Awake()
    {
        obstacleResource = transform; // 현재 ObstacleResource 오브젝트의 Transform
        LoadObstacles();
        SaveObstacles();
    }


    private void LoadObstacles()
    {
        obstalceResource = new Dictionary<string, GameObject>();

        // Resources 폴더에서 빌딩 프리팹 한 번에 로드
        GameObject[] obstalces = Resources.LoadAll<GameObject>("Obstacles");

        foreach (GameObject obstacle in obstalces)
        {
            obstalceResource[obstacle.name] = obstacle;  // 이름으로 Dictionary 저장
        }

    }

    private void SaveObstacles()
    {
        foreach (var kvp in obstalceResource)
        {
            string obstacleName = kvp.Key;
            GameObject obstaclePrefab = kvp.Value;

            // 빌딩 프리팹 인스턴스 생성
            GameObject newObstacle = Instantiate(obstaclePrefab);

            // 현재 오브젝트 하위에 옵스타큽 생성
            newObstacle.transform.SetParent(this.transform);

            // 이름을 프리팹과 동일하게 설정
            newObstacle.name = obstacleName;

            newObstacle.gameObject.SetActive(false);
        }
    }


    public GameObject GetRandomObstacleFromChildren(string filterKeyword)
    {
        filteredObstacles.Clear();

        if (obstacleResource.childCount == 0) return null;

        foreach (Transform child in obstacleResource)
        {
            if (child.name.Contains(filterKeyword))
            {
                filteredObstacles.Add(child);
            }
        }

        if (filteredObstacles.Count == 0)
        {
            LoadObstacles();
            SaveObstacles();

            foreach (Transform child in obstacleResource)
            {
                if (child.name.Contains(filterKeyword))
                {
                    filteredObstacles.Add(child);
                }
            }

        }

        // 랜덤으로 하나 선택
        randomObstacle = filteredObstacles[Random.Range(0, filteredObstacles.Count)];
        return randomObstacle.gameObject;
    }



}
