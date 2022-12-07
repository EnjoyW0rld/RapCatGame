using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraxction : MonoBehaviour,IClickable
{
    [SerializeField] private EnemyPersona p;
    [SerializeField] private int nextScene;
    [SerializeField] private int rpToEnter;
    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        if (rpToEnter > GameInformation.Instance.reputationPoints)
        {
            print("No enter");
            return;
        }
        GameInformation.Instance.SetEnemyPersona(p);
        MySceneManager.SetScene(nextScene);
    }
    public void OnHover()
    {

    }
}
