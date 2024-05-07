using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundManagerInitializer : MonoBehaviour
{
    [FormerlySerializedAs("_musicGroup")] public SoundDataGroup MusicGroup;
    [FormerlySerializedAs("_fxGroup")] [Space] public SoundDataGroup FxGroup;
    [FormerlySerializedAs("_uiGroup")] [Space] public SoundDataGroup UiGroup;
    
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
        CreateSounds("Musics", MusicGroup.Data, _soundManager.Musics, MusicGroup.MixerGroup);
    
    public void CreateFXSoundsSource() => 
        CreateSounds("FXSounds", FxGroup.Data, _soundManager.FXSounds, FxGroup.MixerGroup);
    
    public void CreateUISoundsSource() => 
        CreateSounds("UISounds", UiGroup.Data, _soundManager.UISounds, UiGroup.MixerGroup);
    
    private void CreateSounds(string parentName, IEnumerable<SoundData> soundsData, ICollection<SoundGroup> container, AudioMixerGroup mixerGroup)
    {
        var soundGroupParent = GetOrCreateSoundGroup(parentName);
        
        foreach (var data in soundsData)
        {
            var sound = GetOrCreateSoundInChildrenWithName(soundGroupParent, data.Name);
            sound.Name = data.Name;
            sound.AudioSources = new List<AudioSource>();
            
            AddExistingAudioSources(sound);
            var extraInstanceNumber = data.InstanceNumber - sound.AudioSources.Count; 
            for (var i = 0; i < extraInstanceNumber ; i++)
            {
                var newInstance = CreateInstance($"Instance {i}", data, mixerGroup);
                newInstance.transform.parent = sound.transform;
                sound.AudioSources.Add(newInstance);
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

    private SoundGroup GetOrCreateSoundInChildrenWithName(Transform parent, string soundName)
    {
        var sounds = parent.GetComponentsInChildren<SoundGroup>();
        var sound =  sounds.FirstOrDefault(sound => sound.Name == soundName);
        if (sound == null)
        {
            var go = new GameObject(soundName);
            go.transform.parent = parent;
            sound = go.AddComponent<SoundGroup>();
        }
        return sound;
    }

    private AudioSource CreateInstance(string instanceName, SoundData data, AudioMixerGroup mixerGroup)
    {
        var soundInstance = new GameObject(instanceName);
        var audioSource = soundInstance.AddComponent<AudioSource>();
        audioSource.loop = data.Loop;
        audioSource.clip = data.AudioClip;
        audioSource.playOnAwake = data.PlayOnAwake;
        audioSource.outputAudioMixerGroup = mixerGroup;
        return audioSource;
    }

    private void AddExistingAudioSources(SoundGroup soundGroup)
    {
        var children = soundGroup.GetComponentsInChildren<AudioSource>();
        foreach (var child in children)
        {
            soundGroup.AudioSources.Add(child);
        }
    }
}
