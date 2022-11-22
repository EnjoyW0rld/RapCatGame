using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    TextMeshProUGUI text;
    int score;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScore()
    {
        score += 5;
        text.text = "Current score: " + score;
    }
}
