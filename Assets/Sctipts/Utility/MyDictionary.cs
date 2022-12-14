using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyDictionary : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI textPlace;
    [SerializeField] private TextMeshProUGUI secondTextPlace;
    [SerializeField] private MyButton[] buttons;
    [SerializeField] private GameObject pageNumber;

    private int currentPage = 0;
    public UnityEvent OnOpen;
    public UnityEvent OnClose;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!canvas.enabled) WriteText();
            TurnCanvasOn();
        }
    }

    void WriteText()
    {
        AddButtons();
        textPlace.text = " ";
        secondTextPlace.text = " ";
        int wordsWritten = 0;
        int currentBox = 0;
        TextMeshProUGUI[] boxes = { textPlace, secondTextPlace };
        foreach (var item in GameInformation.Instance.learnedWords)
        {
            if (wordsWritten == 5)
            {
                currentBox++;
                wordsWritten = 0;
            }
            boxes[currentBox].text += "<b>" + item.Key + "</b>";

            boxes[currentBox].text += item.Value;
            boxes[currentBox].text += "\n";
            wordsWritten++;
        }
    }
    void TurnCanvasOn()
    {
        if (!canvas.enabled)
        {
            print("on open");
            OnOpen?.Invoke();
        }
        else
        {
            print("on close");
            OnClose?.Invoke();
        }
    }
    void AddButtons()
    {
        if (GameInformation.Instance.learnedWords.Count > 4)
        {
            GetButton("next").gameObject.SetActive(currentPage == 0);
        }
    }
    MyButton GetButton(string name)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == name) return buttons[i];
        }
        return null;
    }
    public void OnClick()
    {
        TurnCanvasOn();
        WriteText();
    }
}

[Serializable]
public class MyButton
{
    public Button button;
    public string name;
    public GameObject gameObject { get { return button.gameObject; } }
}
