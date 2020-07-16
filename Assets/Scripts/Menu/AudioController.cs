using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Serializable]
    public enum Sounds
    {
        Button,
        GameOver,
        OpenUi, 
        Dead,
        Reload,
        Shoot, 
        Collect_1,
        Hit,
        EnemyDead
    }

    public List<AudioElement> SoundElements = new List<AudioElement>();

    public static AudioController Instance;
    public AudioSource UISoundSource;
    public AudioSource ThemeSoundSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(Sounds type)
    {
        AudioElement c = SoundElements.FirstOrDefault(e => e.SoundType == type);

        if( c != null)
        {
            UISoundSource.PlayOneShot(c.SoundClip);
        }
    }

    public void PlayTheme()
    {
        // ThemeSoundSource.Play();
    }

    [Serializable]
    public class AudioElement
    {
        public AudioClip SoundClip;
        public Sounds SoundType;
    }
}
