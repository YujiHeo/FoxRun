using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public Transform cameraPoint;

    Camera mainCamera;

    static bool isFeverTime = false;

    void Awake()
    {
        GameManager.instance.player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();

        controller.speed = () => condition.Speed;
        controller.jumpPower = () => condition.JumpPower;
        controller.getStat = () => condition.stat;
        controller.changeStat = (x) => condition.stat = x;

        condition.deadAction += () => controller.anim.SetTrigger("Dead");

        mainCamera = Camera.main;
        if (mainCamera != null && cameraPoint != null)
        {
            mainCamera.transform.parent = cameraPoint;
            mainCamera.transform.localPosition = Vector3.zero;
            mainCamera.transform.localRotation = Quaternion.identity;
            mainCamera.farClipPlane = 100f;
        }
    }

    void OnDestroy()
    {
        if (mainCamera != null)
            mainCamera.transform.parent = null;
    }

    public IEnumerator StartFeverTime(float duration)
    {
        isFeverTime = true;
        GameManager.instance.feverTimeScore = 2;
        Debug.Log($"피버 타임에 진입했습니다. 플레이어가 얻는 점수는 {GameManager.instance.feverTimeScore}배가 되었습니다.");

        yield return new WaitForSeconds(duration);

        GameManager.instance.feverTimeScore = 1;
        isFeverTime = false;
        Debug.Log($"피버 타임이 끝났습니다. 플레이어가 얻는 점수는 {GameManager.instance.feverTimeScore}배가 되었습니다.");
    }
}
