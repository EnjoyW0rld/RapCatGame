using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextWriting : MonoBehaviour
{
    public UnityEvent<int> onComplete;
    public UnityEvent onError;
    public UnityEvent OnNewWordAppear;
    public UnityEvent OnSentenceStart;
    public UnityEvent OnCorrectLetter;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI timeGUI;

    [SerializeField] private string fadedColour = "#c0c0c0ff";
    [SerializeField] private string finishedColour = "#ff0000ff";

    private Queue<string[]> currentQueue;
    private string[] sentence;
    private int[] values;

    private char[] currentWord;
    private int lettersTyped = 0;
    private int currentStreak = 0;
    private bool fightStarted;

    //time variables
    [SerializeField] private float timeForWord;
    private float timeLeft;
    //Other script instances
    private EnemyFightLogic enemy;
    private GeneralFightLogic fightL;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<EnemyFightLogic>();
        timeLeft = timeForWord;
        values = GameInformation.Instance.keyValues;

        //variable for enemy fight logic
        fightL = FindObjectOfType<GeneralFightLogic>();
        //get queue of words for this fight
        currentQueue = WordsParser.GetPlayerBattleQueue(enemy.getCurrentPersona().getName());
        SetNewWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fightStarted) return;
        if (fightL.isEnemyTurn) return;
        KeyCode k = KeyCode.None;

        TimeManager();

        if (Input.anyKeyDown)
        {
            k = GameInformation.Instance.GetPressedKey();//GetPressedKey();
            if (k == KeyCode.None) return;
            if (isLetterCorrect((char)k))
            {
                OnCorrectLetter?.Invoke();
                lettersTyped++;
            }
            else
            {
                OnTimeElapsed();
            }
            if (lettersTyped == currentWord.Length)
            {
                WordComplete();
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
        onError?.Invoke();
        lettersTyped = 0;
        currentStreak = 0;
        timeGUI.text = "Time left: " + 0;
        timeLeft = timeForWord;
        fightL.ChangeTurn(true);
    }
    void SetNewWord()
    {
        sentence = currentQueue.Dequeue();
        OnSentenceStart?.Invoke();
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

        text.text += "<color=" + fadedColour + ">"; //grey text open tag
        text.text += "<color=" + finishedColour + ">"; //red text open tag

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (isFirstTime)
            {
                if (i == lettersTyped) text.text += "</color>";
                text.text += currentWord[i];
            }
            else
            {
                if (i == lettersTyped) text.text += "</color>";
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
    public static void ShowText(ref TextMeshProUGUI textGUI, string[] sentence, int lettersTyped)
    {
        bool isFirstTime = !GameInformation.Instance.KnowWord(sentence[1]);
        char[] _currentWord = sentence[1].ToCharArray();


        textGUI.text = "";
        textGUI.text += sentence[0]; //first part of sentence add

        textGUI.text += "<color=#c0c0c0ff>"; //grey text open tag
        textGUI.text += "<color=#ff0000ff>"; //red text open tag

        for (int i = 0; i < _currentWord.Length; i++)
        {
            if (isFirstTime)
            {
                if (i == lettersTyped) textGUI.text += "</color>";
                textGUI.text += _currentWord[i];
            }
            else
            {
                if (i == lettersTyped) textGUI.text += "</color>";
                if (i == 0)
                {
                    textGUI.text += _currentWord[i];
                }
                else if (i < lettersTyped) textGUI.text += _currentWord[i];
                else textGUI.text += "_";

            }
        }


        textGUI.text += "</color>";
        textGUI.text += sentence[2];
    }
    void WordComplete()
    {
        timeLeft = timeForWord;
        if (WordsParser.HasExplanation(sentence[1]))
        GameInformation.Instance.AddToSeen(sentence[1]);
        lettersTyped = 0;
        currentStreak++;
        onComplete?.Invoke(currentStreak);
        if (currentQueue.Count > 0)
            SetNewWord();

    }
    public void SetFightState(bool started) => fightStarted = started;
}
