using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocationMove : MonoBehaviour
{
    [SerializeField] private int RPtoProceed;
    [SerializeField, ContextMenuItem("Set original pos", "SetOriginalPos")] private Vector3 OriginalPos;
    [SerializeField, ContextMenuItem("Set future pos", "SetFuturePos")] private Vector3 positionToBe;
    public UnityEvent OnFailToMove;

    void Start()
    {
        Camera.main.transform.position = OriginalPos;
    }

    // Update is called once per frame
    public void OnClick()
    {
        if (GameInformation.Instance.reputationPoints >= RPtoProceed)
        {
            MoveToFuturePos();
        }
        else
        {
            OnFailToMove?.Invoke();
        }
    }
    void MoveToFuturePos()
    {
        Camera.main.transform.position = positionToBe;
    }
    void SetFuturePos()
    {
        positionToBe = Camera.main.transform.position;
    }
    void SetOriginalPos()
    {
        OriginalPos = Camera.main.transform.position;
    }
}