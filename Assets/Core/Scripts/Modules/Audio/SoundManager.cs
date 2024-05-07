using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance => _instance;
    private static SoundManager _instance;
    
    [HideInInspector] public List<SoundGroup> Musics;
    [HideInInspector] public List<SoundGroup> FXSounds;
    [HideInInspector] public List<SoundGroup> UISounds;

    private AudioSource _playingMusicSource;
    
    public void Init()
    {
        Musics = new List<SoundGroup>();
        FXSounds = new List<SoundGroup>();
        UISounds = new List<SoundGroup>();
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
        MarkAndPlayMusicSource(Musics[random].TakeFreeSource());
    }
    
    public void PlayMusic(string musicName)
    {
        if (TryGetAudioSource(musicName, Musics, out var music))
        {
            MarkAndPlayMusicSource(music);
        }
        else
        {
            Debug.LogError($"Music with name {musicName} not found");
        }
    }

    public void RestartMusic()
    {
        StopMusic();
        MarkAndPlayMusicSource(_playingMusicSource);
    }

    public void StopMusic()
    {
        if (_playingMusicSource != null && _playingMusicSource.isPlaying)
            _playingMusicSource.Stop();
    }

    public void PlayFX(string fxName, Vector3? position = null)
    {
        if (TryGetAudioSource(fxName, FXSounds, out var fx, position))
        {
            fx.PlayOneShot(fx.clip);
            if (position != null)
                fx.transform.position = position.Value;
        }
        else
        {
            Debug.LogError($"Sound FX with name {fxName} not found");
        }
    }

    public void PlayUISound(string uiSoundName, Vector3? position = null)
    {
        if (TryGetAudioSource(uiSoundName, UISounds, out var uiSound, position))
        {
            uiSound.PlayOneShot(uiSound.clip);
            if (position != null)
                uiSound.transform.position = position.Value;
        }
        else
        {
            Debug.LogError($"UI Sound with name {uiSoundName} not found");
        }
    }

    private bool TryGetAudioSource(string soundName, IEnumerable<SoundGroup> container, out AudioSource sound,
        Vector3? playPoint = null)
    {
        sound = null;
        var soundGroup = container.FirstOrDefault(sound => sound.Name == soundName);
        var hasGroup = soundGroup != null;
        if (hasGroup)
            sound = soundGroup.TakeFreeSource(playPoint);
    
        return hasGroup;
    }
    
    private void MarkAndPlayMusicSource(AudioSource audioSource)
    {
        StopMusic();
        _playingMusicSource = audioSource;
        _playingMusicSource.Play();
    }
    
}
