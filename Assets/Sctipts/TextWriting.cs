using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextWriting : MonoBehaviour
{
    public UnityEvent<int> onComplete;
    [SerializeField] TextMeshProUGUI text;
    //[SerializeField] string[] words;
    string[] sentence;
    int[] values;

    [SerializeField] char[] currentWord;
    [SerializeField] int lettersTyped = 0;

    [SerializeField] int currentStreak = 0;
    // Start is called before the first frame update
    void Start()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        SetNewWord();
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode k = KeyCode.None;

        if (Input.anyKeyDown)
        {
            k = GetPressedKey();

            if (isLetterCorrect((char)k))
            {
                lettersTyped++;
            }
            else
            {
                currentStreak = 0;
                lettersTyped = 0;
            }
            if (lettersTyped == currentWord.Length)
            {
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

    void SetNewWord()
    {
        //sentence = new string[] { "Cmn bro "};
        //int num = Random.Range(0, words.Length);
        //currentWord = words[num].ToCharArray();
        currentWord = WordsParser.GetRandomString().ToCharArray();
        ShowText();
        //text.text = words[num];

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
        text.text += "<color=#ff0000ff>";
        for (int i = 0; i < currentWord.Length; i++)
        {
            text.color = Color.white;
            if (i == lettersTyped)
            {
                text.text += "</color>";
            }

            text.text += currentWord[i];
        }

    }
}
