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
}
