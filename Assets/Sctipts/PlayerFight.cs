using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFight : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDeath;
    public UnityEvent OnDamage;
    [SerializeField] private int maxHP;
    int currentHp;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHP;
        FindObjectOfType<EnemyFightLogic>().OnWordComplete.AddListener(GetDamage);
    }

    void GetDamage()
    {
        currentHp -= 10;
        OnDamage?.Invoke();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
    }
    public int getMaxHp() => maxHP;
}
