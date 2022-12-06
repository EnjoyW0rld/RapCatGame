using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] private int sceneToChange;

    private void Start()
    {
        player.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (!player.isPlaying && player.frame > 1) MySceneManager.SetScene(sceneToChange);
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) MySceneManager.SetScene(sceneToChange);
    }
}