using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetScene(int sceneNumber)
    {
        if(sceneNumber > SceneManager.sceneCount)
        {
            Debug.LogError("Invalid scene number");
            return;
        }
        SceneManager.LoadScene(sceneNumber);
    }
}
