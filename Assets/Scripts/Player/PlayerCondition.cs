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
    int hp;
    public int Hp => hp;
    [SerializeField] float invinTime;
    float lastHurtTime;

    public Action damageAction;
    public Action deadAction;

    void Awake()
    {
        hp = maxHp;
    }

    public void GetDamage(int _damage)
    {
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
}
