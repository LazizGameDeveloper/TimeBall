using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerInitializer : MonoBehaviour
{
    [SerializeField] private SoundDataGroup _musicGroup;
    [Space] [SerializeField] private SoundDataGroup _fxGroup;
    [Space] [SerializeField] private SoundDataGroup _uiGroup;
    
    [SerializeField] private bool _destroyComponentAfterInit;
    [SerializeField] private SoundManager _soundManager;
    
    public void Awake()
    {
        _soundManager.Init();
        CreateMusicsSource();
        CreateFXSoundsSource();
        CreateUISoundsSource(); 
        if (_destroyComponentAfterInit)
            Destroy(this);
    }

    public void CreateMusicsSource() => 
        CreateSounds("Musics", _musicGroup.Data, _soundManager.Musics, _musicGroup.MixerGroup);
    
    public void CreateFXSoundsSource() => 
        CreateSounds("FXSounds", _fxGroup.Data, _soundManager.FXSounds, _fxGroup.MixerGroup);
    
    public void CreateUISoundsSource() => 
        CreateSounds("UISounds", _uiGroup.Data, _soundManager.UISounds, _uiGroup.MixerGroup);

    private void CreateSounds(string parentName, IEnumerable<SoundData> soundsData, ICollection<Sound> container, AudioMixerGroup mixerGroup)
    {
        var soundGroupParent = GetOrCreateSoundGroup(parentName);
        
        foreach (var data in soundsData)
        {
            var sound = GetOrCreateSoundInChildrenWithName(soundGroupParent, data.Name);
            sound.Name = data.Name;
            if (!sound.TryGetComponent<AudioSource>(out _))
            {
                var audioSource = sound.gameObject.AddComponent<AudioSource>();
                audioSource.loop = data.Loop;
                audioSource.clip = data.AudioClip;
                audioSource.playOnAwake = data.PlayOnAwake;
                audioSource.outputAudioMixerGroup = mixerGroup;
                sound.AudioSource = audioSource;
            }
            container.Add(sound);
        }
    }

    private Transform GetOrCreateSoundGroup(string parentName)
    {
        var children = GetComponentsInChildren<Transform>();
        
        var parent = children.FirstOrDefault(child => child.name == parentName);
        if (parent == null)
        {
            var go = new GameObject(parentName);
            go.transform.parent = transform;
            parent = go.transform;
        }
        return parent;
    }

    private Sound GetOrCreateSoundInChildrenWithName(Transform parent, string soundName)
    {
        var sounds = parent.GetComponentsInChildren<Sound>();
        var sound =  sounds.FirstOrDefault(sound => sound.Name == soundName);
        if (sound == null)
        {
            var go = new GameObject(soundName);
            go.transform.parent = parent;
            sound = go.AddComponent<Sound>();
        }
        return sound;
    }
}
