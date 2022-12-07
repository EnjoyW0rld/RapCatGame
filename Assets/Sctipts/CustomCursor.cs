using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D baseCursor;
    [SerializeField] private Vector2 offset = new Vector2(16,0);
    private void Start()
    {
        Cursor.SetCursor(baseCursor, offset, CursorMode.ForceSoftware);
    }
}