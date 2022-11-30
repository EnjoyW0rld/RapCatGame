using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool isRandom;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void PlayRandom()
    {
        m_AudioSource.clip = clips[Random.Range(0, clips.Length)];
        m_AudioSource.Play();
    }
    public void PlaySound()
    {
        if (m_AudioSource.clip != clips[0]) m_AudioSource.clip = clips[0];
        m_AudioSource.Play();
    }
    public void PlaySound(int index)
    {
        if (index > clips.Length - 1)
        {
            Debug.LogError("invalid index");
            return;
        }
        if (m_AudioSource.clip != clips[index]) m_AudioSource.clip = clips[index];
        m_AudioSource.Play();
    }
}
