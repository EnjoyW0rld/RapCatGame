using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconToChange;
    [SerializeField] private Sprite[] spriteAtZero;
    [SerializeField] private Sprite[] defaultSprite;
    private bool isZero;
    private void Start()
    {
        if (iconToChange == null)
        {
            Debug.LogError("No icon to change assigned");
            return;
        }
    }

    public void ChangeSprite(float value)
    {
        isZero = value == 0;
        SetSprite(isZero ? spriteAtZero[0] : defaultSprite[0]);
    }

    public void SwitchOnOFF()
    {
        switch (gameObject.active)
        {
            case true:
                SetSprite(isZero ? spriteAtZero[1] : defaultSprite[1]);
                break;
            case false:
                SetSprite(isZero ? spriteAtZero[0] : defaultSprite[0]);
                break;
        }
        gameObject.SetActive(!gameObject.active);
    }
    void SetSprite(Sprite spr)
    {
        iconToChange.sprite = spr;
    }
    public void OnPointerEnter()
    {
        gameObject.SetActive(true);
        SetSprite(isZero ? spriteAtZero[0] : defaultSprite[0]);
    }
    public void OnPointerExit()
    {
        gameObject.SetActive(false);
        SetSprite(isZero ? spriteAtZero[1] : defaultSprite[1]);
    }
}
