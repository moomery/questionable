using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private TextMeshProUGUI ValueText;
    [SerializeField] private AudioMixMode MixMode;

    public void OnChangeSlider(float value)
    {
        ValueText.SetText($"{value.ToString("N2")}");

        switch (MixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
            AudioSource.volume = value;
            break;
            case AudioMixMode.LinearMixerVolume:
            Mixer.SetFloat("Volume", (-80 + value * 100));
            break;
            case AudioMixMode.LogrithmicMixerVolume:
            Mixer.SetFloat ("Volume",Mathf.Log10(value)*20);
            break;
        }
    }

    public enum AudioMixMode
    {
        LinearAudioSourceVolume, 
        LinearMixerVolume,
        LogrithmicMixerVolume
    }
}
