using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyPersona
{
    [SerializeField] private int health;
    [SerializeField] private float timeToRead;
    [SerializeField] private string name = "nothing";

    public void GetDamage(int d)
    {
        health -= d;
    }

    public int getHealth() => health;
    public bool isDead() => health <= 0;
    public float getTimeToRead() => timeToRead;
    public string getName() => name;
}
//The first rule of Fight Club is: you do not talk about Fight Club.
//The second rule of Fight Club is: you DO NOT talk about Fight Club!
//Third rule of Fight Club: someone yells "stop!", goes limp, taps out, the fight is over.
//Fourth rule: only two guys to a fight.
