using UnityEngine;
using IJunior.TypedScenes;

public class LevelBuilder : MonoBehaviour, ISceneLoadHandler<Level>
{
    public void OnSceneLoaded(Level level)
    {
        Instantiate(level);
    }
}
