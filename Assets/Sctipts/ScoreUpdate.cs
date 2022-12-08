using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(text != null)
        {
            text.text = "Current score: 0";
        }
        FindObjectOfType<TextWriting>().onComplete.AddListener(UpdateScore);
    }

    void UpdateScore(int streak)
    {
        score += 5;
        text.text = "Current score: " + score;
    }
}
