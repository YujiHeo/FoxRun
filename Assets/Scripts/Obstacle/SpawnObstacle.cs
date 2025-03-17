using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstacle;
    public GameObject[] item;
    public List<float> spawnX;
    public float spawnZ = 120f;
    public float ItemspawnY = 1f;
    public float obstacleSpawnY = 0;

    private float spawnInterval; // �⺻ ���� ����
    public float minSpawnInterval = 0.3f; // �ּ� ���� ���� (�ʹ� ������ �ʵ��� ����)
    public float speedFactor = 0.05f; // �÷��̾� �ӵ��� ���� ����

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
            float adjustedInterval = Mathf.Max(minSpawnInterval, spawnInterval - speedFactor); // �ּҰ� ����
            yield return new WaitForSeconds(adjustedInterval); // �������� ���� �ֱ� ����
        }
    }

    private void SpawnOb()
    {
        if (spawnX == null || spawnX.Count == 0)
        {
            Debug.LogError("spawnX �迭�� ��� �ֽ��ϴ�! �ν����Ϳ��� �� ������ �ʿ��մϴ�.");
            return;
        }

        number = Random.Range(0, 100);

        if(number > 20)
        {
            ItemObstacle(obstacle, obstacleSpawnY);
     
        }
        else
        {
            ItemObstacle(item, ItemspawnY);
        }

    }


    private void ItemObstacle(GameObject[] _select, float _sapwY)
    {

        int index = Random.Range(0, _select.Length); // �迭���� �������� ����
        int indexPosition = Random.Range(0, spawnX.Count);
        float _spawnX = spawnX[indexPosition];

        Vector3 spawnPosition = new Vector3(_spawnX, _sapwY, spawnZ);
        GameObject newObstacle = Instantiate(_select[index], spawnPosition, Quaternion.identity);

        newObstacle.transform.SetParent(this.transform);

    }


}
