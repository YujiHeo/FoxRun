using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoadMove: MonoBehaviour
{
    public List<Transform> roadObject;
    public float roadSpeed;
    public float resetPositionZ = -10f;  // 도로가 이 위치까지 오면 맨 앞으로 이동
    public float startPositionZ = 10f;   // 도로를 맨 앞으로 배치할 위치

    private void Update()
    {
        for(int i = 0; i < roadObject.Count; i++)
        {
            roadObject[i].transform.Translate(Vector3.back * roadSpeed * Time.deltaTime);

            // 특정 위치에 도달하면 맨 앞으로 이동
            if (roadObject[i].position.z <= resetPositionZ)
            {
                RepositionRoad(roadObject[i]);
            }
        }

    }

    private void RepositionRoad(Transform road)
    {
        // 가장 마지막 도로 조각의 위치를 기준으로 배치
        Transform lastRoad = roadObject[roadObject.Count - 1];
        road.position = new Vector3(lastRoad.position.x, lastRoad.position.y, lastRoad.position.z + 20f);

        // 리스트에서 제거 후 다시 추가하여 순서 유지
        roadObject.Remove(road);
        roadObject.Add(road);
    }
}
