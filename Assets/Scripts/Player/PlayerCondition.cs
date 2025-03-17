using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PSTAT
{
    RUN,
    JUMP,
    SLIDE,
    DEAD
}

public class PlayerCondition : MonoBehaviour
{
    [Header("MovementInfo")]
    [SerializeField] float speed;
    public float Speed => speed;
    [SerializeField] float jumpPower;
    public float JumpPower => jumpPower;

    public PSTAT stat = PSTAT.RUN;

    [Header("DamageInfo")]
    [SerializeField] int maxHp;
    public int MaxHp => maxHp;
    [SerializeField] int hp;
    public int Hp => hp;
    [SerializeField] float invinTime;
    float lastHurtTime;

    public Action damageAction;
    public Action deadAction;

    [SerializeField] public bool isFeverTime = false;
    [SerializeField] public bool isInvincibleTime = false;
    [SerializeField] public static bool isMagnet = false;

    void Awake()
    {
        hp = maxHp;
    }

    public void GetDamage(int _damage)
    {
        if (isInvincibleTime == true)
        {
            return;
        }

        if (Time.time - lastHurtTime < invinTime) return;
        lastHurtTime = Time.time;

        hp -= _damage;
        damageAction?.Invoke();
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        stat = PSTAT.DEAD;
        deadAction?.Invoke();
    }

    public IEnumerator StartFeverTime(PlayerCondition player, float duration)
    {
        yield return new WaitForSeconds(duration);

        GameManager.instance.feverTimeScore = 1;
        player.isFeverTime = false;
    }

    public IEnumerator StartInvincibleTime(PlayerCondition player, float duration)
    {
        Debug.Log("무적이 되었습니다!");

        //float originalSpeed = speed;

        //speed = originalSpeed * 2;

        yield return new WaitForSeconds(duration);

        //speed = originalSpeed;

        PlayerCondition.isMagnet = false;
        player.isInvincibleTime = false;
        Debug.Log("무적 끝!!");
    }
}
