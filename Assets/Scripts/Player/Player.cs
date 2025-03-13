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
        Debug.Log($"�ǹ� Ÿ�ӿ� �����߽��ϴ�. �÷��̾ ��� ������ {GameManager.instance.feverTimeScore}�谡 �Ǿ����ϴ�.");

        yield return new WaitForSeconds(duration);

        GameManager.instance.feverTimeScore = 1;
        isFeverTime = false;
        Debug.Log($"�ǹ� Ÿ���� �������ϴ�. �÷��̾ ��� ������ {GameManager.instance.feverTimeScore}�谡 �Ǿ����ϴ�.");
    }
}
