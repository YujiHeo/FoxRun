using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MapMove: MonoBehaviour
{
    public List<Transform> roadObject;
    public MapElementData roadData;
    public float resetPositionZ = -10f;  // ���ΰ� �� ��ġ���� ���� �� ������ �̵�
    public float startPositionZ = 10f;   // ���θ� �� ������ ��ġ�� ��ġ

    private void Update()
    {
        for(int i = 0; i < roadObject.Count; i++)
        {
            roadObject[i].transform.Translate(Vector3.back * roadData.moveSpeed * Time.deltaTime);

            // Ư�� ��ġ�� �����ϸ� �� ������ �̵�
            if (roadObject[i].position.z <= resetPositionZ)
            {
                RepositionRoad(roadObject[i]);
            }
        }

    }

    private void RepositionRoad(Transform road)
    {
        // ���� ������ ���� ������ ��ġ�� �������� ��ġ
        Transform lastRoad = roadObject[roadObject.Count - 1];
        road.position = new Vector3(lastRoad.position.x, lastRoad.position.y, lastRoad.position.z + startPositionZ);

        // ����Ʈ���� ���� �� �ٽ� �߰��Ͽ� ���� ����
        roadObject.Remove(road);
        roadObject.Add(road);
    }
}
