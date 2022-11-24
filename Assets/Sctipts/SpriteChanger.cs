using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color c = Color.gray;
    [SerializeField] bool greyWhenTrue;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) Debug.LogError("No sprite renderer found");
        FindObjectOfType<GeneralFightLogic>().OnTurnChange.AddListener(SetColour);
    }

    public void SetColour(bool isEnemyTurn)
    {
        if (!isEnemyTurn)
        {
            if (greyWhenTrue)
                spriteRenderer.color = Color.white;
            else spriteRenderer.color = c;
        }
        else
        {
            if (greyWhenTrue)
            {
                spriteRenderer.color = c;

            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }

    }
}
