using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneRoadMove : MonoBehaviour
{
    public List<Transform> roadObject;
    public float roadSpeed;
    public float resetPositionX;  // 도로가 이 위치까지 오면 맨 앞으로 이동
    public float startPositionX; // 도로를 맨 앞으로 배치할 위치

    private void Update()
    {
        for (int i = 0; i < roadObject.Count; i++)
        {
            roadObject[i].transform.Translate(Vector3.forward * roadSpeed * Time.deltaTime);

            // 특정 위치에 도달하면 맨 앞으로 이동
            if (roadObject[i].transform.position.x <= resetPositionX)
            {
                RepositionRoad(roadObject[i]);
            }
        }

    }

    private void RepositionRoad(Transform road)
    {
        // 가장 마지막 도로 조각의 위치를 기준으로 배치
        Transform lastRoad = roadObject[roadObject.Count - 1];
        road.transform.position = new Vector3(lastRoad.transform.position.x + 40f, lastRoad.transform.position.y, lastRoad.transform.position.z);

        // 리스트에서 제거 후 다시 추가하여 순서 유지
        roadObject.Remove(road);
        roadObject.Add(road);
    }
}

