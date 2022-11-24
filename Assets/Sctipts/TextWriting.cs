using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextWriting : MonoBehaviour
{
    public UnityEvent<int> onComplete;
    public UnityEvent OnNewWordAppear;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI timeGUI;
    
    string[] sentence;
    int[] values;

    char[] currentWord;
    int lettersTyped = 0;
    int currentStreak = 0;

    //time variables
    [SerializeField]float timeForWord;
    float timeLeft;
    //Other script instances
    EnemyFightLogic enemy;
    GeneralFightLogic fightL;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<EnemyFightLogic>();

        timeLeft = timeForWord;
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        fightL = FindObjectOfType<GeneralFightLogic>();
        SetNewWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (fightL.isEnemyTurn) return;
        KeyCode k = KeyCode.None;

        TimeManager();

        if (Input.anyKeyDown)
        {
            k = GetPressedKey();

            if (isLetterCorrect((char)k))
            {
                lettersTyped++;
            }
            else
            {
                OnTimeElapsed();
                currentStreak = 0;
                lettersTyped = 0;
            }
            if (lettersTyped == currentWord.Length)
            {
                timeLeft = timeForWord;
                SetNewWord();
                lettersTyped = 0;
                currentStreak++;
                onComplete?.Invoke(currentStreak);
            }
            ShowText();
        }

        ShowText();
    }

    KeyCode GetPressedKey()
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)values[i]))
            {
                return (KeyCode)values[i];
            }
        }
        return KeyCode.None;
    }

    void TimeManager()
    {
        timeLeft -= Time.deltaTime;
        timeGUI.text = "Time left: " + timeLeft;
        if (timeLeft <= 0)
        {
            OnTimeElapsed();
        }
    }
    void OnTimeElapsed()
    {
        timeGUI.text = "Time left: " + 0;
        timeLeft = timeForWord;
        //enemy.ChangeTurn(true);
        print("elapsed");
        fightL.ChangeTurn(true);
    }
    void SetNewWord()
    {
        sentence = WordsParser.GetRandomSentence(); //get random sentence from pool

        //check if word has explanation and is not on the dictionary yet
        if (WordsParser.HasExplanation(sentence[1]) && !GameInformation.Instance.IsInDictionary(sentence[1])) 
        {
            GameInformation.Instance.AddWord(sentence[1],WordsParser.GetExplanation(sentence[1]));
            OnNewWordAppear?.Invoke();
        }
        currentWord = sentence[1].ToCharArray();
        ShowText();
    }
    bool isLetterCorrect(char c)
    {
        return c == currentWord[lettersTyped] || char.ToUpper(c) == currentWord[lettersTyped];
    }
    void ShowText()
    {
        text.text = " ";
        if (sentence != null)
        {
            text.text += sentence[0];
        }
        text.text += "<color=#c0c0c0ff>";
        text.text += "<color=#ff0000ff>";
        for (int i = 0; i < currentWord.Length; i++)
        {
            //text.color = Color.white;
            if (i == lettersTyped)
            {
                text.text += "</color>";
            }

            text.text += currentWord[i];
        }
        text.text += "</color>";
        if (sentence != null)
        {
            text.text += sentence[2];
        }

    }
}
