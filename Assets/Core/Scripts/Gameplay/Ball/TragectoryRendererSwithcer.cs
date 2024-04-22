using UnityEngine;
using UnityEngine.Serialization;

public class TragectoryRendererSwithcer : MonoBehaviour
{
    [FormerlySerializedAs("_ballContoller")] [SerializeField] private BallController _ballController;

    private void Update()
    {
        if (!_ballController.gameObject.activeInHierarchy && gameObject.activeInHierarchy)
            gameObject.SetActive(false);

        if (_ballController.gameObject.activeInHierarchy && !gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}