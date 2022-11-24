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
    // Start is called before the first frame update
    void Start()
    {
        maxLenght = healthPivot.localScale.x;
        switch (entity)
        {
            case Entity.Character:
                break;
            case Entity.Enemy:
                EnemyFightLogic fl = FindObjectOfType<EnemyFightLogic>();
                maxHP = fl.getCurrentPersona().getHealth();
                fl.OnGetDamage.AddListener(OnGetDamage);
                break;
        }
        currentHP = maxHP;
    }

    void OnGetDamage(int damage)
    {
        currentHP -= damage;
        float newScaleX = Mathf.Lerp(0, maxLenght,currentHP/maxHP);
        print(newScaleX);
        //healthPivot.localScale.Set(newScaleX, healthPivot.localScale.y, healthPivot.localScale.z);
        healthPivot.localScale = new Vector3(newScaleX, healthPivot.localScale.y, healthPivot.localScale.z);
    }
}
