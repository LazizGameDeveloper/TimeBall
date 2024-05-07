using System.Management.Instrumentation;
using UnityEngine;

namespace Extensions
{
    public static class AudioSourceExtensions
    {
        public static void CopyParametersFrom(this AudioSource source, AudioSource target)
        {
            source.outputAudioMixerGroup = target.outputAudioMixerGroup;
            source.clip = target.clip;
            source.volume = target.volume;
            source.pitch = target.pitch;
            source.loop = target.loop;
        }
    }
}