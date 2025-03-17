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
    public Player player;
    [Range(30, 100)] public float moveSpeed;
    [Range(0.1f, 1f)] public float spawnGap;
    [Range(0,1)] public float addmoveSpeed;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Update()
    {
        obstacleData.moveSpeed = moveSpeed;
        roadData.moveSpeed = moveSpeed;
        builddingData.moveSpeed = moveSpeed;
        spawnObstacle.minSpawnInterval = spawnGap;

        if(player.condition.Hp <=0)
        {
            moveSpeed = 0f;
        }


        if(moveSpeed >= 100f)
        {
            moveSpeed = 100f;
        }
        else
        {
            moveSpeed += Time.deltaTime * addmoveSpeed;
        }

        if(moveSpeed >= 40f)
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


