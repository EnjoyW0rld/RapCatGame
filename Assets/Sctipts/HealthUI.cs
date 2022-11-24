using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Transform healthPivot;
    enum Entity { Character, Enemy };
    [SerializeField] Entity entity;

    int maxHP;
    float maxLenght;
    float currentHP;

    void Start()
    {
        maxLenght = healthPivot.localScale.x;
        switch (entity)
        {
            case Entity.Character:
                PlayerFight plF = FindObjectOfType<PlayerFight>();
                plF.OnDamage.AddListener(OnGetDamage);
                maxHP = plF.getMaxHp();
                break;
            case Entity.Enemy:
                EnemyFightLogic fl = FindObjectOfType<EnemyFightLogic>();
                maxHP = fl.getCurrentPersona().getHealth();
                fl.OnGetDamage.AddListener(OnGetDamage);
                break;
        }
        currentHP = maxHP;
    }


    void OnGetDamage()
    {
        OnGetDamage(10);
    }
    void OnGetDamage(int damage)
    {
        currentHP -= damage;
        float newScaleX = Mathf.Lerp(0, maxLenght,currentHP/maxHP);
        healthPivot.localScale = new Vector3(newScaleX, healthPivot.localScale.y, healthPivot.localScale.z);
    }
}
