using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GeneralFightLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameBox;
    private string characterName = "Millie the Silly";
    public UnityEvent<bool> OnTurnChange;

    private bool _isEnemyTurn;
    public bool isEnemyTurn { get => _isEnemyTurn; }

    private void Start()
    {
        if (nameBox == null) Debug.LogError("No text box for names assigned");
    }

    public void ChangeTurn(bool isEnemyTurn)
    {
        string nameToShow;
        if (isEnemyTurn)
        {
            nameToShow = FindObjectOfType<EnemyFightLogic>().getCurrentPersona().getName();
            print("current name " + nameToShow);
        }
        else
        {
            nameToShow = characterName;
        }
        this._isEnemyTurn = isEnemyTurn;
        nameBox.text = nameToShow + ":";
        OnTurnChange?.Invoke(isEnemyTurn);
    }
}
