using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraxction : MonoBehaviour
{

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
        print("kabooom");
        //MySceneManager.SetScene(2);
    }
    void CheckClick()
    {

    }
}
