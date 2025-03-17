using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleResource : MonoBehaviour
{
    private Dictionary<string, GameObject> obstalceResource;
    Transform obstacleResource; // ���� ObstacleResource ������Ʈ�� Transform
    List<Transform> filteredObstacles = new List<Transform>(); // Ư�� Ű���带 �����ϴ� �ڽ� ������Ʈ ����Ʈ
    Transform randomObstacle;

    private void Awake()
    {
        obstacleResource = transform; // ���� ObstacleResource ������Ʈ�� Transform
        LoadObstacles();
        SaveObstacles();
    }


    private void LoadObstacles()
    {
        obstalceResource = new Dictionary<string, GameObject>();

        // Resources �������� ���� ������ �� ���� �ε�
        GameObject[] obstalces = Resources.LoadAll<GameObject>("Obstacles");

        foreach (GameObject obstacle in obstalces)
        {
            obstalceResource[obstacle.name] = obstacle;  // �̸����� Dictionary ����
        }

    }

    private void SaveObstacles()
    {
        foreach (var kvp in obstalceResource)
        {
            string obstacleName = kvp.Key;
            GameObject obstaclePrefab = kvp.Value;

            // ���� ������ �ν��Ͻ� ����
            GameObject newObstacle = Instantiate(obstaclePrefab);

            // ���� ������Ʈ ������ �ɽ�ŸŮ ����
            newObstacle.transform.SetParent(this.transform);

            // �̸��� �����հ� �����ϰ� ����
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

        // �������� �ϳ� ����
        randomObstacle = filteredObstacles[Random.Range(0, filteredObstacles.Count)];
        return randomObstacle.gameObject;
    }



}
