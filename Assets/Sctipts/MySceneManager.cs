using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static void SetScene(int sceneNumber)
    {
        if(sceneNumber > SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Invalid scene number, tried access " + sceneNumber + " out of " + SceneManager.sceneCountInBuildSettings);
            return;
        }
        SceneManager.LoadScene(sceneNumber);
    }
    public static void SetScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
