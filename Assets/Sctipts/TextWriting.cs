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

    Queue<string[]> currentQueue;
    string[] sentence;
    int[] values;

    char[] currentWord;
    int lettersTyped = 0;
    int currentStreak = 0;

    //time variables
    [SerializeField] float timeForWord;
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
        currentQueue = WordsParser.GetCurrentBattleQueue(true, enemy.getCurrentPersona().getName());
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
                GameInformation.Instance.AddToSeen(sentence[1]);
                SetNewWord();
                lettersTyped = 0;
                currentStreak++;
                onComplete?.Invoke(currentStreak);
            }
            //ShowText();
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
        timeGUI.text = "" + System.Math.Round(timeLeft, 2);
        if (timeLeft <= 0)
        {
            OnTimeElapsed();
        }
    }
    void OnTimeElapsed()
    {
        lettersTyped = 0;
        timeGUI.text = "Time left: " + 0;
        timeLeft = timeForWord;
        fightL.ChangeTurn(true);
    }
    void SetNewWord()
    {
        sentence = currentQueue.Dequeue();

        //check if word has explanation and is not on the dictionary yet
        if (WordsParser.HasExplanation(sentence[1]) && !GameInformation.Instance.IsInDictionary(sentence[1]))
        {
            GameInformation.Instance.AddWord(sentence[1], WordsParser.GetExplanation(sentence[1]));
            OnNewWordAppear?.Invoke();
        }
        currentWord = sentence[1].ToCharArray();
        ShowText();
    }
    bool isLetterCorrect(char c)
    {
        return c == currentWord[lettersTyped] || char.ToUpper(c) == currentWord[lettersTyped];
    }
    void ShowText(bool f = true)
    {
        bool isFirstTime = !GameInformation.Instance.KnowWord(sentence[1]);//GameInformation.Instance.IsInDictionary(sentence[1]);
        text.text = " ";
        if (sentence != null)
        {
            text.text += sentence[0];
        }
        text.text += "<color=#c0c0c0ff>"; //grey text open tag
        text.text += "<color=#ff0000ff>"; //red text open tag
        //text.text += currentWord[0];

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (isFirstTime)
            {
                //text.color = Color.white;
                if (i == lettersTyped)
                {
                    text.text += "</color>"; //red text close tag
                }
                text.text += currentWord[i];
            }
            else
            {
                if (i < lettersTyped)
                {
                    text.text += currentWord[i];
                }
                else
                {
                    text.text += '_';
                }

            }
            /*
            //text.color = Color.white;
            if (i == lettersTyped)
            {
                text.text += "</color>"; //red text close tag
            }
            text.text += currentWord[i];
             */

        }

        text.text += "</color>";
        //text.text += "</color>";
        if (sentence != null)
        {
            text.text += sentence[2];
        }

    }
    void ShowText()
    {
        bool isFirstTime = !GameInformation.Instance.KnowWord(sentence[1]);
        
        text.text = "";
        text.text += sentence[0]; //first part of sentence add

        text.text += "<color=#c0c0c0ff>"; //grey text open tag
        text.text += "<color=#ff0000ff>"; //red text open tag

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (isFirstTime)
            {
                if(i == lettersTyped) text.text += "</color>";
                text.text += currentWord[i];
            }
            else
            {
                if(i == lettersTyped) text.text += "</color>";
                if (i == 0)
                {
                    text.text += currentWord[i];
                }
                else if (i < lettersTyped) text.text += currentWord[i];
                else text.text += "_";

            }
        }


        text.text += "</color>";
        text.text += sentence[2];
    }
}
