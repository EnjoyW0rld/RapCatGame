using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerValueChange : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMasterVolume(float value)
    {
        float realValue = Mathf.Lerp(-80, 0, value);
        mixer.SetFloat("VolumeMaster",realValue);
    }
}
