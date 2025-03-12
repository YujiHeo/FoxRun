using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    int hp;

    public void GetDamage(int _damage)
    {
        hp -= _damage;
        if (hp == 0)
            Die();
    }

    public void Die()
    { }
}
