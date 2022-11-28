using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyDictionary : MonoBehaviour, IClickable
{
    //static MyDictionary instance;
    //public static MyDictionary Instance { get { return instance; } }
    //Dictionary<string, string> dict;

    //[SerializeField] GameObject dictionaryScreen;
    //[SerializeField] TextMeshProUGUI textPlace;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI textPlace;
    void Awake()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvas.enabled = !canvas.enabled;
            if (canvas.enabled) WriteText();
        }
    }

    void WriteText()
    {
        textPlace.text = " ";
        foreach (var item in GameInformation.Instance.learnedWords)
        {
            textPlace.text += item.Key + item.Value;
            textPlace.text += "\n";
        }
    }

    public void OnClick()
    {
        WriteText();
    }
}
