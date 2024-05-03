using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance => _instance;
    private static SoundManager _instance;
    
    [HideInInspector] public List<Sound> Musics;
    [HideInInspector] public List<Sound> FXSounds;
    [HideInInspector] public List<Sound> UISounds;
    
    public void Init()
    {
        Musics = new List<Sound>();
        FXSounds = new List<Sound>();
        UISounds = new List<Sound>();
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }
#if DEBUG
        Debug.LogError($"There is more than 1 AudioManger, {name} have been destroyed");    
#endif
        Destroy(gameObject);
    }
    
    public void PlayRandomMusic()
    {
        var random = Random.Range(0, Musics.Count);
        Musics[random].AudioSource.Play();
    }
    
    public void PlayMusic(string musicName)
    {
        if (TryGetSound(musicName, Musics, out var music))
            music.AudioSource.Play();
    }

    public void PlayFX(string fxName)
    {
        if (TryGetSound(fxName, FXSounds, out var fx))
        {
            fx.AudioSource.PlayOneShot(fx.AudioSource.clip);
        }
    }

    public void PlayUISound(string uiSoundName)
    {
        if (TryGetSound(uiSoundName, UISounds, out var uiSound))
        {
            uiSound.AudioSource.PlayOneShot(uiSound.AudioSource.clip);
        }
    }

    private bool TryGetSound(string soundName, List<Sound> container, out Sound sound)
    {
        sound = Musics.FirstOrDefault(music => music.Name == soundName);
        return sound != null;
    }
}
