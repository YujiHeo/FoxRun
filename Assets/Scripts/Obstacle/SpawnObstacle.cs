using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstacle;
    public GameObject[] item;
    public List<float> spawnX;
    public float spawnZ = 120f;

    private float spawnInterval; // 기본 생성 간격
    public float minSpawnInterval = 0.3f; // 최소 생성 간격 (너무 빠르지 않도록 제한)
    public float speedFactor = 0.05f; // 플레이어 속도에 따라 감소

    private int number;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnObstacleRoutine());

        
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            SpawnOb();
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

        number = Random.Range(0, 100);

        if(number > 50)
        {
            ItemObstacle(obstacle);
        }
        else
        {
            ItemObstacle(item);
        }

        //    int index = Random.Range(0, obstacle.Length); // 배열에서 랜덤으로 장애물 선택
        //int indexPosition = Random.Range(0, spawnX.Count);
        //float _spawnX = spawnX[indexPosition];

        //Vector3 spawnPosition = new Vector3(_spawnX, 0, spawnZ);
        //GameObject newObstacle = Instantiate(obstacle[index], spawnPosition, Quaternion.identity);

        //newObstacle.transform.SetParent(this.transform);

    }


    private void ItemObstacle(GameObject[] select)
    {

        int index = Random.Range(0, select.Length); // 배열에서 랜덤으로 선택
        int indexPosition = Random.Range(0, spawnX.Count);
        float _spawnX = spawnX[indexPosition];

        Vector3 spawnPosition = new Vector3(_spawnX, 0, spawnZ);
        GameObject newObstacle = Instantiate(select[index], spawnPosition, Quaternion.identity);

        newObstacle.transform.SetParent(this.transform);

    }


}
