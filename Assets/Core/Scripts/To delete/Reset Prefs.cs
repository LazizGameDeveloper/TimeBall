using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
