using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFightLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    EnemyPersona persona;
    int maxHp;

    void Start()
    {
        persona = GameInformation.Instance.GetCurrentEnemy();
        FindObjectOfType<TextWriting>().onComplete.AddListener(OnWordComplete);

        if (persona != null) maxHp = persona.getHealth();
    }

    void OnWordComplete(int streak)
    {
        int additionalDamage = streak % 5 == 0 ? 10 : 0;
        persona.GetDamage(10 + additionalDamage);
        if (persona.isDead())
        {
            int additionalRP = streak % 10 == 0 ? 20 : 0;
            GameInformation.Instance.UpdateRP(10 + additionalRP);
        }
        hpText.text = "Current health: " + persona.getHealth() + "/" + maxHp;
    }

}
