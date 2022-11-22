using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] UnityEvent onTimerEnd;
    [SerializeField] float timeLeft;
    [SerializeField] bool isRepeating;
    bool isDone;
    float originalTime;

    private void Start()
    {
        originalTime = timeLeft;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDone)
        {
            Iterate();
        }
    }
    void Iterate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            onTimerEnd?.Invoke();
            if (isRepeating)
            {
                timeLeft = originalTime;
            }
            else
            {
                isDone = true;
            }
        }
    }
}
