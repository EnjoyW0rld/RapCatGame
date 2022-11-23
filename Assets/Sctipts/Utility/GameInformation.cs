using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{

    private static GameInformation instance;
    EnemyPersona pers;

    [HideInInspector]public Dictionary<string, string> learnedWords { get; private set; }
    [HideInInspector] public int reputationPoints { get; private set; }

    public static GameInformation Instance { get => instance; }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            learnedWords = new Dictionary<string, string>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public EnemyPersona GetCurrentEnemy()
    {
        return pers;
    }
    public void SetEnemyPersona(EnemyPersona p)
    {
        pers = p;
    }
    public void UpdateRP(int toAdd)
    {
        reputationPoints += toAdd;
    }

    public bool IsInDictionary(string word)
    {
        return learnedWords.ContainsKey(word);
    }
    public void AddWord(string word, string explanation)
    {
        learnedWords.Add(word, explanation);
    }
}
