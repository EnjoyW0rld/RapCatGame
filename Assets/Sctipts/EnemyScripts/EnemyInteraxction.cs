using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraxction : MonoBehaviour
{
    [SerializeField] EnemyPersona p;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnInteract();
        }
    }
    public void OnInteract()
    {
        GameInformation.Instance.SetEnemyPersona(p);
        MySceneManager.SetScene(2);
    }
    void CheckClick()
    {

    }
}
