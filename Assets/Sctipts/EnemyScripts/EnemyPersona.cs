using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyPersona
{
    [SerializeField] int health;
    [SerializeField] float typeSpeed;

    public void GetDamage(int d)
    {
        health -= d;
    }

    public int getHealth() => health;
    public bool isDead() => health <= 0;
    public float getTypeSpeed() => typeSpeed;
}
