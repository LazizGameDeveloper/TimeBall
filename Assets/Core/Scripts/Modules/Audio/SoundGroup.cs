using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

public class SoundGroup : MonoBehaviour
{
    public string Name;
    public List<AudioSource> AudioSources;

    public AudioSource TakeFreeSource(Vector3? sourcePosition = null)
    {
        var free = AudioSources.FirstOrDefault(source => !source.isPlaying || source.transform.position == sourcePosition);
        if (free == null)
        {
            free = DuplicateFirstElement();
        }
        Debug.Log($"Taken element: {free.name}");
        return free;
    }

    private AudioSource DuplicateFirstElement()
    {
        var go = new GameObject("Instance" + AudioSources.Count);
        var component = go.AddComponent<AudioSource>();
        component.CopyParametersFrom(AudioSources[0]);
        AudioSources.Add(component);
        go.transform.parent = transform;
        return component;
    }
}
