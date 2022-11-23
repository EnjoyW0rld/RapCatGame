using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraxction : MonoBehaviour,IClickable
{
    [SerializeField] EnemyPersona p;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        GameInformation.Instance.SetEnemyPersona(p);
        MySceneManager.SetScene(2);
    }
}
