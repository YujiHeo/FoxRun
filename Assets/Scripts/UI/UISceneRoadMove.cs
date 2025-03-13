using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneRoadMove : MonoBehaviour
{
    public List<Transform> roadObject;
    public float roadSpeed;
    public float resetPositionX;  // ���ΰ� �� ��ġ���� ���� �� ������ �̵�
    public float startPositionX; // ���θ� �� ������ ��ġ�� ��ġ

    private void Update()
    {
        for (int i = 0; i < roadObject.Count; i++)
        {
            roadObject[i].transform.Translate(Vector3.forward * roadSpeed * Time.deltaTime);

            // Ư�� ��ġ�� �����ϸ� �� ������ �̵�
            if (roadObject[i].transform.position.x <= resetPositionX)
            {
                RepositionRoad(roadObject[i]);
            }
        }

    }

    private void RepositionRoad(Transform road)
    {
        // ���� ������ ���� ������ ��ġ�� �������� ��ġ
        Transform lastRoad = roadObject[roadObject.Count - 1];
        road.transform.position = new Vector3(lastRoad.transform.position.x + 40f, lastRoad.transform.position.y, lastRoad.transform.position.z);

        // ����Ʈ���� ���� �� �ٽ� �߰��Ͽ� ���� ����
        roadObject.Remove(road);
        roadObject.Add(road);
    }
}

