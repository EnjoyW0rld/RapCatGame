using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StreakDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    private void Start()
    {
        TextWriting tw = FindObjectOfType<TextWriting>();
        tw.onComplete.AddListener(AddStreak);
        tw.onError.AddListener(EraseStreak);
    }
    void EraseStreak()
    {
        textBox.text = "0";
    }
    void AddStreak(int streak)
    {
        textBox.text = "X" + streak; 
    }

}
