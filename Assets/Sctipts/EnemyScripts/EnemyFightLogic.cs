using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFightLogic : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnWordComplete;
    [HideInInspector] public UnityEvent<int> OnGetDamage;

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI wordsWrite;

    EnemyPersona persona;
    int maxHp;

    GeneralFightLogic fightL;

    string[] sentence;
    char[] currentWord;
    int lettersTyped;

    float toUpdate;

    void Start()
    {
        persona = GameInformation.Instance.GetCurrentEnemy();
        FindObjectOfType<TextWriting>().onComplete.AddListener(GetDamage);
        fightL = FindObjectOfType<GeneralFightLogic>();

        if (persona != null)
        {
            maxHp = persona.getHealth();
            toUpdate = persona.getTypeSpeed();
        }
        SetNewWord();
    }


    private void Update()
    {
        if (!fightL.isEnemyTurn) return;
        toUpdate -= Time.deltaTime;
        if (toUpdate <= 0)
        {
            toUpdate = persona.getTypeSpeed();
            int r = Random.Range(0, 100);
            if (r >= 20)
            {
                lettersTyped++;
                ShowText();
            }
            else
            {
                fightL.ChangeTurn(false);
                lettersTyped = 0;
                ShowText();
            }
            if (lettersTyped == currentWord.Length)
            {
                OnWordComplete?.Invoke();
                lettersTyped = 0;
                SetNewWord();
            }
        }
    }

    void SetNewWord()
    {
        sentence = WordsParser.GetRandomSentence(false);
        currentWord = sentence[1].ToCharArray();
        ShowText();
    }

    void ShowText()
    {
        wordsWrite.text = " ";
        if (sentence != null) wordsWrite.text += sentence[0];
        wordsWrite.text += "<color=#c0c0c0ff>";
        wordsWrite.text += "<color=#ff0000ff>";
        for (int i = 0; i < currentWord.Length; i++)
        {
            if (i == lettersTyped)
            {
                wordsWrite.text += "</color>";
            }
            wordsWrite.text += currentWord[i];
        }
        wordsWrite.text += "</color>";
        if (sentence != null) wordsWrite.text += sentence[2];
    }
    void GetDamage(int streak)
    {
        int additionalDamage = streak % 5 == 0 ? 10 : 0;
        persona.GetDamage(10 + additionalDamage);
        if (persona.isDead())
        {
            int additionalRP = streak % 10 == 0 ? 20 : 0;
            GameInformation.Instance.UpdateRP(10 + additionalRP);
            MySceneManager.SetScene(1);
        }
        hpText.text = "Current health: " + persona.getHealth() + "/" + maxHp;
        OnGetDamage?.Invoke(10 + additionalDamage);
    }

    public EnemyPersona getCurrentPersona() => persona;
}
