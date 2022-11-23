using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyDictionary : MonoBehaviour,IClickable
{
    //static MyDictionary instance;
    //public static MyDictionary Instance { get { return instance; } }
    //Dictionary<string, string> dict;

    [SerializeField] GameObject dictionaryScreen;
    [SerializeField] TextMeshProUGUI textPlace;
    void Awake()
    {
     /*   if (instance == null)
        {
      //      instance = this;
      //      DontDestroyOnLoad(gameObject);
            //dict = new Dictionary<string, string>();
            //dict.Add("ssssssss", "dsadsadsa");
            //dict.Add(";slsls", "mkgfdklkldf");
            //dict.Add("peopop", "klfdsepwo");
        }
        else
        {
            Destroy(gameObject);
     }*/   
    }

    /*public bool IsInDictionary(string word)
    {
        return dict.ContainsKey(word);
    }
    public void AddWord(string word, string explanation)
    {
        dict.Add(word, explanation);
    }*/
    void WriteText()
    {
        textPlace.text = " ";
        foreach (var item in GameInformation.Instance.learnedWords)
        {
            textPlace.text += item.Key + " - " + item.Value;
            textPlace.text += "\n";
        }
    }

    public void OnClick()
    {
        WriteText();
    }
}
