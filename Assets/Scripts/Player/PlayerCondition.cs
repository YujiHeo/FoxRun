using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float speed;
    public float Speed => speed;
    [SerializeField] float jumpPower;
    public float JumpPower => jumpPower;

    public void GetDamage(int _damage)
    {
        hp -= _damage;
        if (hp == 0)
            Die();
    }

    public void Die()
    { }
}
