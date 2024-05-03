using System.Collections;
using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    public IEnumerator Start()
    {
        yield return null;
        SoundManager.Instance.PlayRandomMusic();
    }
}