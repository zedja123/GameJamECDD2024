using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        PlayerAttack,
        PlayerHurt,
        ShieldDeflect,
        SlimeShoot,
        Stairs,
        TrackBattle,
        TrackBoss,
        TrackMenu,
        Victory,
        Lock,

    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void PlaySound(Sound sound)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("One Shot Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));


        /*  GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        */
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
}
