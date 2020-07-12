using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public enum Sound {
        ButtonClick,
        ButtonLock,
        ButtonUnlock,
        PlayerMove,
        PlayerJump,
        PlayerLand,
        PlayerCrouch,
        PlayerUncrouch,
        PlayerInteract,
        PlayerDessolve,
        PlayerRespawn,
        Dialog,
        Hack,
        FlameThrowerShoot,
        FlameThrowerReload,
        TurretShoot,
        TurretReload
    };

    public SoundAudioClip[] audioClips = new SoundAudioClip[0];
    public static SoundAudioClip[] soundAudioClips;

    private void Awake()
    {
        soundAudioClips = audioClips;
        SoundPlayer.Initialize();
    }
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public void PlayButtonSound()
    {
        SoundPlayer.PlaySound(Sound.ButtonClick);
    }

}




