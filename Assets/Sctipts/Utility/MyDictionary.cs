using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MyDictionary : MonoBehaviour, IClickable
{
    //static MyDictionary instance;
    //public static MyDictionary Instance { get { return instance; } }
    //Dictionary<string, string> dict;

    //[SerializeField] GameObject dictionaryScreen;
    //[SerializeField] TextMeshProUGUI textPlace;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI textPlace;
    public UnityEvent OnOpen;
    public UnityEvent OnClose;
    void Awake()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TurnCanvasOn();
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
    void TurnCanvasOn()
    {
        canvas.enabled = !canvas.enabled;
        if(canvas.enabled) OnOpen?.Invoke();
        else OnClose?.Invoke();
    }

    public void OnClick()
    {
        TurnCanvasOn();
        WriteText();
    }
}
