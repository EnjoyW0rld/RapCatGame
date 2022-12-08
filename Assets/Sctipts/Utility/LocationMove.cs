using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocationMove : MonoBehaviour
{
    [SerializeField] private int RPtoProceed;
    [SerializeField, ContextMenuItem("Set original pos", "SetOriginalPos")] private Vector3 OriginalPos;
    [SerializeField, ContextMenuItem("Set future pos", "SetFuturePos")] private Vector3 positionToBe;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    public UnityEvent OnFailToMove;
    private bool enoughPoints;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enoughPoints = GameInformation.Instance.reputationPoints >= RPtoProceed;
        Camera.main.transform.position = OriginalPos;
        OnHoverExit();
    }

    // Update is called once per frame
    public void OnClick()
    {
        if (enoughPoints)
        {
            MoveToFuturePos();
        }
        else
        {
            OnFailToMove?.Invoke();
        }
    }

    //1 - Light on
    //3 - Light off
    public void OnHoverEnter()
    {
        spriteRenderer.sprite = enoughPoints ? sprites[1] : sprites[3];
    }
    public void OnHoverExit()
    {
        spriteRenderer.sprite = enoughPoints ? sprites[0] : sprites[2];
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