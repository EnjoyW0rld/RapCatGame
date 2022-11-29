using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private int spritesCount;
    [SerializeField] private int currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        spritesCount = sprites.Length;
        spriteRenderer.sprite = sprites[currentSprite];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) SwapForward();
        if (Input.GetKeyDown(KeyCode.S)) SwapBack();
    }
    public void SwapForward()
    {
        currentSprite++;
        if (currentSprite > spritesCount - 1) currentSprite = 0;
        spriteRenderer.sprite = sprites[currentSprite];
    }
    public void SwapBack()
    {
        currentSprite--;
        if (currentSprite < 0) currentSprite = spritesCount - 1;
        spriteRenderer.sprite = sprites[currentSprite];
    }
}
