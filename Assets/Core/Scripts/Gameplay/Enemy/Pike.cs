using UnityEngine;

public class Pike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallController controller))
        {
            controller.Die();
        }
    }
}
