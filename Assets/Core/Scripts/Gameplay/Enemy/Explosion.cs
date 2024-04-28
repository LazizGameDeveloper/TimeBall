using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void Explode(Vector3 position, float radius, LayerMask layerMask)
    {
        var hits = Physics.OverlapSphere(position, radius, layerMask);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<BallController>(out var bar))
                bar.Die();
        }
    }
}
