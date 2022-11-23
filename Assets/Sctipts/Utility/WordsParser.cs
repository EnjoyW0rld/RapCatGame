using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordsParser : MonoBehaviour
{
    [SerializeField] string path = "test.txt";
    [SerializeField] static string[] words;
    [SerializeField] static List<string[]> sentences;
    [SerializeField] static Dictionary<string, string> allWordsExpl;

    // Start is called before the first frame update
    void Awake()
    {
        allWordsExpl = new Dictionary<string, string>();
        words = GetStrings(path);
        sentences = ParseSentences("sentence.txt");
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
    public static string GetRandomString()
    {
        int r = Random.Range(0, words.Length);
        return words[r];
    }

    List<string[]> ParseSentences(string _path)
    {
        if (!File.Exists(_path))
        {
            Debug.LogError("File not found, new blank created!");
            File.Create(_path);
        }
        List<string[]> tmpSent = new List<string[]>();
        using (StreamReader sr = new StreamReader(_path))
        {
            string str;
            while ((str = sr.ReadLine()) != null)
            {
                string[] tmp;
                if (str.Contains('-'))
                {
                    string[] expl = str.Split('-');
                    tmp = Subdivide(expl[0]);
                    allWordsExpl.Add(tmp[1], expl[1]);

                }
                else
                {
                    tmp = Subdivide(str);
                }
                tmpSent.Add(tmp);
            }
        }

        //Converting array
        return tmpSent;
    }
    string[] Subdivide(string str)
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
    public static string[] GetRandomSentence()
    {
        int r = Random.Range(0, sentences.Count);
        return sentences[r];
    }

    public static string GetExplanation(string word)
    {
        return allWordsExpl[word];
    }

}
