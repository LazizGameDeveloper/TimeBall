using UnityEngine;

[System.Serializable]
public class SoundData
{
    public string Name;
    public AudioClip AudioClip;
    public bool PlayOnAwake;
    public bool Loop;
    public int InstanceNumber = 1;
}