using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileChange : MonoBehaviour
{
    [SerializeField] private Sprite millieProfile;
    [SerializeField] private Sprite enemyProfile;
    [SerializeField] private SpriteRenderer profilePlace;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GeneralFightLogic>().OnTurnChange.AddListener(ChangeProfile);
    }

    void ChangeProfile(bool isEnemyTurn)
    {
        profilePlace.sprite = isEnemyTurn ? enemyProfile : millieProfile;
    }
}
