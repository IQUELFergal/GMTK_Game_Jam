using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer
{
    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<SoundManager.Sound, float>();
        soundTimerDictionary[SoundManager.Sound.PlayerMove] = 0f;
    }

    public static void PlaySound(AudioClip audioClip)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }

    public static void PlaySound(SoundManager.Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static AudioClip GetAudioClip(SoundManager.Sound sound)
    {
        foreach(SoundManager.SoundAudioClip soundAudioClip in SoundManager.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }

    private static Dictionary<SoundManager.Sound, float> soundTimerDictionary;

    public static bool CanPlaySound(SoundManager.Sound sound)
    {
        switch (sound)
        {
            default:
                return true;

            case SoundManager.Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.05f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else return false;
                }
                else return true;
        }
    }
    
}