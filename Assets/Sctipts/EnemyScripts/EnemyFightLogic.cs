using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFightLogic : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnWordComplete;
    public UnityEvent OnEnemyDeath;
    public UnityEvent<int> OnGetDamage;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI wordsWrite;
    private Queue<string> battlePhrases;

    private EnemyPersona persona;
    private int maxHp;

    private GeneralFightLogic fightL;

    private string sentence;

    private float toUpdate;

    void Start()
    {
        persona = GameInformation.Instance.GetCurrentEnemy();
        FindObjectOfType<TextWriting>().onComplete.AddListener(GetDamage);
        fightL = FindObjectOfType<GeneralFightLogic>();
        fightL.OnTurnChange.AddListener(OnTurnChange);

        if (persona != null)
        {
            maxHp = persona.getHealth();
            toUpdate = persona.getTimeToRead();
        }
        battlePhrases = WordsParser.GetEnemyBattleQueue(persona.getName());
        //SetNewWord();
    }


    private void Update()
    {
        if (!fightL.isEnemyTurn) return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            fightL.ChangeTurn(false);
        }
        toUpdate -= Time.deltaTime;
        if (toUpdate < 0)
        {
            toUpdate = persona.getTimeToRead();
            //OnWordComplete?.Invoke();
            fightL.ChangeTurn(false);
        }
        
        ShowText();
    }

    void SetNewWord()
    {
        sentence = battlePhrases.Dequeue();
        ShowText();
    }

    void ShowText()
    {
        wordsWrite.text = sentence;
    }

    void GetDamage(int streak)
    {
        int additionalDamage = streak % 5 == 0 ? 10 : 0;
        persona.GetDamage(10 + additionalDamage);
        if (persona.isDead())
        {
            int additionalRP = streak % 10 == 0 ? 20 : 0;
            GameInformation.Instance.UpdateRP(10 + additionalRP);
            OnEnemyDeath?.Invoke();
            //MySceneManager.SetScene(1);
        }
        hpText.text = "Current health: " + persona.getHealth() + "/" + maxHp;
        OnGetDamage?.Invoke(10 + additionalDamage);
    }
    public EnemyPersona getCurrentPersona() => persona;
    void OnTurnChange(bool isEnemyTurn)
    {
        if (isEnemyTurn)
        {
            OnWordComplete?.Invoke();
            SetNewWord();
        }
    }

}
