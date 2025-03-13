using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    static bool isFeverTime = false;

    void Awake()
    {
        GameManager.instance.player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
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
