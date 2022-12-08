using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScale : MonoBehaviour
{
    [SerializeField] private Transform scalingObject;
    private TextWriting tw;
    private float timeForWord;
    private float originalScale;

    void Start()
    {
        tw = FindObjectOfType<TextWriting>();
        timeForWord = tw.GetTimeForWord();
        if (tw == null) Debug.LogError("NO file found");
        originalScale = scalingObject.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        float timeValue = tw.GetTimeLeft() / timeForWord;
        float scaleToSet = Mathf.Lerp(0, originalScale, timeValue);
        scalingObject.localScale = new Vector3(scalingObject.localScale.x, scaleToSet, scalingObject.localScale.z);
    }
}
