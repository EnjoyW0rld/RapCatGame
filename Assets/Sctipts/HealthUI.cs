using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Transform healthPivot;
    enum Entity { Character, Enemy };
    [SerializeField] private Entity entity;
    [SerializeField] private Color[] gradientColours;
    [SerializeField] private SpriteRenderer sprRend;

    private int maxHP;
    private float maxLenght;
    private float currentHP;

    void Start()
    {
        sprRend = GetComponentInChildren<SpriteRenderer>();
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
        float newScaleX = Mathf.Lerp(0, maxLenght, currentHP / maxHP);
        healthPivot.localScale = new Vector3(newScaleX, healthPivot.localScale.y, healthPivot.localScale.z);
        int currColour = (int)(currentHP);
        if (gradientColours == null || gradientColours.Length == 0) return;
        print(currColour);
        if(currColour > 66)
        {
            sprRend.color = gradientColours[2];
        }else if(currColour > 33 && currColour < 66)
        {
            sprRend.color = gradientColours[1];
        }
        else
        {
            sprRend.color = gradientColours[0];
        }
    }
}
