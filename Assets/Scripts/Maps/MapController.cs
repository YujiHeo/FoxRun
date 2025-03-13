using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class MapController : MonoBehaviour
{
    public MapElementData obstacleData;
    public MapElementData roadData;
    public MapElementData builddingData;
    public SpawnObstacle spawnObstacle;
    public MeshRenderer[] buildingGround;
    [Range(30, 100)] public float moveSpeed;
    [Range(0.3f, 1f)] public float spawnGap;
    [Range(0,1)] public float addmoveSpeed;


    public void Update()
    {
        obstacleData.moveSpeed = moveSpeed;
        roadData.moveSpeed = moveSpeed;
        builddingData.moveSpeed = moveSpeed;
        spawnObstacle.minSpawnInterval = spawnGap;

        if(moveSpeed >= 100f)
        {
            moveSpeed = 100f;
        }
        else
        {
            moveSpeed += Time.deltaTime * addmoveSpeed;
        }

        if(moveSpeed >= 33f)
        {
            for(int i = 0 ; i < buildingGround.Length; i++)
            {
                StartCoroutine(ChangeColorGradually(Color.gray));
            }
        }
    }

    private IEnumerator ChangeColorGradually(Color targetColor)
    {
        foreach (var obj in buildingGround)
        {
            if (obj != null)
            {
                Color startColor = obj.material.color;
                float t = 0f;

                while (t < 1f)
                {
                    t += Time.deltaTime * 1;
                    obj.material.color = Color.Lerp(startColor, targetColor, t);
                    yield return null;  // 한 프레임 대기
                }

                obj.material.color = targetColor;  // 최종 색상 적용
            }
        }
    }


}


