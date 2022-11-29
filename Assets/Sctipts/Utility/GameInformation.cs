using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{

    private static GameInformation instance;
    EnemyPersona pers;

    [HideInInspector] public Dictionary<string, string> learnedWords { get; private set; }
    [HideInInspector] public int reputationPoints { get; private set; }
    [HideInInspector] List<string> seenWords;
    [HideInInspector] public int[] keyValues { get; private set; }

    float masterVolume = 1;

    public static GameInformation Instance { get => instance; }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            learnedWords = new Dictionary<string, string>();
            seenWords = new List<string>();
            keyValues = (int[])System.Enum.GetValues(typeof(KeyCode));
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
    public void AddToSeen(string word)
    {
        if (!seenWords.Contains(word)) seenWords.Add(word);
    }
    public bool KnowWord(string word) => seenWords.Contains(word);
    public float getMasterVolume() => masterVolume;
    public KeyCode GetPressedKey()
    {
        for (int i = 0; i < keyValues.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)keyValues[i]))
            {
                if ((KeyCode)keyValues[i] == KeyCode.LeftShift) continue;
                return (KeyCode)keyValues[i];
            }
        }
        return KeyCode.None;
    }
}
