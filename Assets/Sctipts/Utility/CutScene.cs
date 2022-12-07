using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    //[SerializeField] private int sceneToChange;
    [SerializeField] private string sceneToChange;
    private void Start()
    {
        player.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (!player.isPlaying && player.frame > 1) CutSceneEnd();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) CutSceneEnd();
    }
    public void CutSceneEnd()
    {
        MySceneManager.SetScene(sceneToChange);
    }
}
