using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TutorialLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject enterButton;
    [SerializeField] private GameObject bubbleGeorge;
    public UnityEvent OnLetterType;
    public UnityEvent OnEnemyTurn;
    public UnityEvent OnPlayerTurn;

    [Header("Profile variables")]
    [SerializeField] private SpriteRenderer profileRenderer;
    [SerializeField] private Sprite millieProfile;
    [SerializeField] private Sprite enemyProfile;

    private string subFolder = "BattleText/";
    private string enemyPath = "TutorialEnemy.txt";
    private string playerPath = "TutorialPlayer.txt";

    private Queue<string[]> playerQueue = new Queue<string[]>();
    private Queue<string> enemyQueue = new Queue<string>();

    private int letterTyped;
    private string[] sentence;
    private char[] currentWord;
    private bool playerTurn = false;

    void Start()
    {
        ParseSentences(subFolder + enemyPath, false);
        ParseSentences(subFolder + playerPath, true);
        ChangeText();
    }

    //shout out to Alexandra Treimane
    void Update()
    {
        if (playerTurn)
        {
            if (Input.anyKeyDown)
            {
                KeyCode k = GameInformation.Instance.GetPressedKey();
                if (isLetterCorrect((char)k))
                {
                    OnLetterType?.Invoke();
                    letterTyped++;
                    TextWriting.ShowText(ref textBox, sentence, letterTyped);
                }
                if (letterTyped == currentWord.Length)
                {
                    letterTyped = 0;
                    GameInformation.Instance.AddToSeen(sentence[1]);
                    if (WordsParser.HasExplanation(sentence[1]) && !GameInformation.Instance.IsInDictionary(sentence[1]))
                    {
                        print(sentence[1] + " has explanation");
                        GameInformation.Instance.AddWord(sentence[1], WordsParser.GetExplanation(sentence[1]));
                    }
                    playerTurn = false;
                    ChangeText();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ChangeText();
            }
        }
    }

    bool isLetterCorrect(char c)
    {
        return c == currentWord[letterTyped];
    }
    void ParseSentences(string _path, bool forPlayer)
    {
        using (StreamReader sr = new StreamReader(_path))
        {
            List<string[]> playerS = new List<string[]>();
            List<string> enemyS = new List<string>();
            string str;
            while ((str = sr.ReadLine()) != null)
            {
                if (forPlayer)
                {
                    if (str.Contains("-"))
                    {
                        string[] expl = str.Split('-');
                        string[] sentence = WordsParser.Subdivide(expl[0]);
                        WordsParser.AddExplanation(sentence[1], expl[1]);
                        //print("added explanation to " + sentence[1]);
                        //print(WordsParser.HasExplanation(sentence[1]));
                        playerS.Add(sentence);
                    }
                    else
                    {
                        string[] sentence = WordsParser.Subdivide(str);
                        playerS.Add(sentence);
                        if(!WordsParser.HasExplanation(sentence[1]))
                        WordsParser.AddToNotOnDictionary(sentence[1]);
                    }
                }
                else
                {
                    enemyS.Add(str);
                }
            }
            if (forPlayer) playerQueue = new Queue<string[]>(playerS);
            else enemyQueue = new Queue<string>(enemyS);
        }
    }
    
    void ChangeText()
    {
        if (enemyQueue.Count == 0)
        {
            MySceneManager.SetScene(nextScene);
            return;
        }
        string enemySentence = enemyQueue.Dequeue();
        if (enemySentence == "-")
        {
            nameBox.text = "Millie the Silly:";
            SetProfile(millieProfile);
            OnPlayerTurn?.Invoke();
            enterButton.SetActive(false);
            if (bubbleGeorge != null) bubbleGeorge.SetActive(true);
            playerTurn = true;
            sentence = playerQueue.Dequeue();
            currentWord = sentence[1].ToCharArray();
            TextWriting.ShowText(ref textBox, sentence, letterTyped);
        }
        else
        {
            OnEnemyTurn?.Invoke();
            enterButton.SetActive(true);
            SetProfile(enemyProfile);
            if (bubbleGeorge != null && bubbleGeorge.active) Destroy(bubbleGeorge);
            nameBox.text = "George the Distinguished II";
            textBox.text = enemySentence;
        }
    }
    void SetProfile(Sprite spr)
    {
        profileRenderer.sprite = spr;
    }
}
