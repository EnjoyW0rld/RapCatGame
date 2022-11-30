using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordsParser : MonoBehaviour
{
    [SerializeField] string pathPlayer = "sentence.txt";
    [SerializeField] string pathEnemy = "test.txt";
    string subFolder = "BattleText/";

    //static List<string[]> sentences;
    //static List<string[]> enemyWords;

    static Dictionary<string, List<string[]>> playerWordsPool;
    static Dictionary<string, List<string>> enemyWordsPool;

    [SerializeField] static Dictionary<string, string> allWordsExpl;
    static List<string> notOnDictionary;

    static bool finishedParsing;

    // Start is called before the first frame update
    void Awake()
    {
        if (finishedParsing) return;
        //Checking if directory exists
        if (!Directory.Exists(subFolder))
        {
            Directory.CreateDirectory(subFolder);
        }
        string[] files = Directory.GetFiles(subFolder);


        //create all the lists
        notOnDictionary = new List<string>();
        allWordsExpl = new Dictionary<string, string>();
        playerWordsPool = new Dictionary<string, List<string[]>>();
        enemyWordsPool = new Dictionary<string, List<string>>();


        for (int i = 0; i < files.Length; i++)
        {
            ParseAndApply(files[i]);
        }

        finishedParsing = true;
    }

    string[] GetStrings(string _path)
    {
        List<string> strings = new List<string>();
        if (!File.Exists(_path))
        {
            File.Create(_path);
        }
        using (StreamReader sr = new StreamReader(_path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                strings.Add(s);
            }

        }
        return strings.ToArray();
    }

    void ParseAndApply(string _path, bool forPlayer = true)
    {
        bool foundTag = false;
        string[] tags = null;

        //temporary lists
        List<string> enemyWords = new List<string>();
        List<string[]> playerWords = new List<string[]>();
        using (StreamReader sr = new StreamReader(_path))
        {
            string str;
            while ((str = sr.ReadLine()) != null)
            {
                if (!foundTag)
                {
                    tags = str.Split(':');
                    foundTag = true;
                    continue;
                }

                switch (tags[0])
                {
                    case "Player":
                        //playerWordsPool.Add(tags[1], ParsePlayer(str));
                        playerWords.Add(ParsePlayer(str));
                        break;
                    case "Enemy":
                        enemyWords.Add(str);
                        //enemyWordsPool.Add(tags[1], str);
                        break;
                }
            }
            //add to dictionary
            switch (tags[0])
            {
                case "Player":
                    playerWordsPool.Add(tags[1], playerWords);
                    break;
                case "Enemy":
                    enemyWordsPool.Add(tags[1], enemyWords);
                    break;
            }
        }

    }
    
    string[] ParsePlayer(string str)
    {
        string[] tmp;
        if (str.Contains('-'))
        {
            string[] expl = str.Split('-');
            tmp = Subdivide(expl[0]);
            AddExplanation(tmp[1], expl[1]);//allWordsExpl.Add(tmp[1].ToLower(), expl[1]);
        }
        else
        {
            tmp = Subdivide(str);
            notOnDictionary.Add(tmp[1].ToLower());
        }
        return tmp;

    }
    public static string[] Subdivide(string str)
    {
        string[] tmp = new string[3];

        //getting first part
        int openIndex = str.IndexOf('<');
        if (openIndex == 0)
        {
            tmp[0] = " ";
        }
        else
        {
            string firstPart = str.Substring(0, openIndex);
            tmp[0] = firstPart;
        }

        //getting second part
        int closeIndex = str.IndexOf('>');
        string secondPart = str.Substring(openIndex + 1, closeIndex - openIndex - 1);
        tmp[1] = secondPart;

        if (closeIndex == str.Length)
        {
            tmp[2] = " ";
        }
        else
        {
            string thirdPart = str.Substring(closeIndex + 1, str.Length - closeIndex - 1);
            tmp[2] = thirdPart;
        }
        return tmp;
    }
    
    public static Queue<string> GetEnemyBattleQueue(string key)
    {
        return new Queue<string>(enemyWordsPool[key]);
    }
    public static Queue<string[]> GetPlayerBattleQueue(string key)
    {
            return new Queue<string[]>(playerWordsPool[key]);
    }
    
    public static string GetExplanation(string word)
    {
        return allWordsExpl[word.ToLower()];
    }
    public static bool HasExplanation(string word) => allWordsExpl.ContainsKey(word.ToLower());//!notOnDictionary.Contains(word.ToLower());

    public static void AddToNotOnDictionary(string word)
    {
        //if (HasExplanation(word.ToLower())) return;
        if (!notOnDictionary.Contains(word.ToLower()))
        {
            notOnDictionary.Add(word.ToLower());
        }
    }
    public static void AddExplanation(string word,string explanation)
    {
        if (!allWordsExpl.ContainsKey(word.ToLower()))
        {
            if (word.ToLower() == "bloody") print("bloody add");
            allWordsExpl.Add(word.ToLower(), explanation);
        }
    }
}
