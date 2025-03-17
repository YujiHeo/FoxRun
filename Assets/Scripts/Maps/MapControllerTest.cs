using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControllerTest : MonoBehaviour
{
    public MapElementData obstacleData;
    public MapElementData roadData;
    public MapElementData builddingData;
    public MapsMovingObstacles movingItmes;
    public MapsMovingObstacles movingObstacles;
    public BuildingMoveRevision building;
    public BuildingMoveRevision treeObjects;
    public MeshRenderer[] buildingGround;
    public Player player;
    public float backgroundChangeTimeGap = 10f;
    [Range(30, 100)] public float moveSpeed;
    [Range(0.1f, 1f)] public float spawnGap;
    [Range(0, 1)] public float addmoveSpeed;
    public ParticleSystem[] seasonPartcle;
    public EnviromentSetting enviromentSetting;

    private void Awake()
    {
        MapManager.Instance.mapControllerTest = this;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enviromentSetting = GetComponentInChildren<EnviromentSetting>();
        StartCoroutine(ChangeColorRoutine());
    }

    public void Update()
    {
        obstacleData.moveSpeed = moveSpeed;
        roadData.moveSpeed = moveSpeed;
        builddingData.moveSpeed = moveSpeed;
        movingObstacles.minSpawnInterval = spawnGap;

        if (player.condition.Hp <= 0)
        {
            moveSpeed = 0f;
            movingItmes.StopSpawning();
            movingObstacles.StopSpawning();
        }


        if (moveSpeed >= 100f)
        {
            moveSpeed = 100f;
        }
        else
        {
            moveSpeed += Time.deltaTime * addmoveSpeed;
        }

    }


    IEnumerator ChangeColorRoutine()
    {
        while (true)
        {
            treeObjects.resourceName = ResourceName.Spring;
            seasonPartcle[3].Stop();
            seasonPartcle[0].Play();
            enviromentSetting.SetMainEnvironment();
            yield return new WaitForSeconds(backgroundChangeTimeGap);
            treeObjects.resourceName = ResourceName.Summer;
            seasonPartcle[0].Stop();
            seasonPartcle[1].Play();
            yield return new WaitForSeconds(backgroundChangeTimeGap);
            treeObjects.resourceName = ResourceName.Fall;
            seasonPartcle[1].Stop();
            seasonPartcle[2].Play();
            yield return new WaitForSeconds(backgroundChangeTimeGap);
            treeObjects.resourceName = ResourceName.Winter;
            seasonPartcle[2].Stop();
            seasonPartcle[3].Play();
            enviromentSetting.SetSnowEnvironment();
            yield return new WaitForSeconds(backgroundChangeTimeGap);
        }
    }





    //private IEnumerator ChangeColorGradually(Color targetColor)
    //{
    //    foreach (var obj in buildingGround)
    //    {
    //        if (obj != null)
    //        {
    //            Color startColor = obj.material.color;
    //            float t = 0f;

    //            while (t < 1f)
    //            {
    //                t += Time.deltaTime * 1;
    //                obj.material.color = Color.Lerp(startColor, targetColor, t);
    //                yield return null;  // 한 프레임 대기
    //            }

    //            obj.material.color = targetColor;  // 최종 색상 적용
    //        }
    //    }
    //}


}
