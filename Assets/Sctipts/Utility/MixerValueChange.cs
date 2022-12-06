using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerValueChange : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] int lowestValue = -40;

    public void SetMasterVolume(float value)
    {
        float realValue = Mathf.Lerp(lowestValue, 0, value);
        realValue = realValue == lowestValue ? -80 : realValue;
        mixer.SetFloat("VolumeMaster",realValue);
    }
}
