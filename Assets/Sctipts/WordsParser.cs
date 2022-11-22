using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordsParser : MonoBehaviour
{
    [SerializeField] string path = "test.txt";
    [SerializeField]static string[] words;

    // Start is called before the first frame update
    void Awake()
    {
        words = GetStrings(path);
    }

    string[] GetStrings(string _path)
    {
        List<string> strings = new List<string>();
        if (!File.Exists(_path))
        {
            File.Create(_path);
        }
        using(StreamReader sr = new StreamReader(_path))
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


}
