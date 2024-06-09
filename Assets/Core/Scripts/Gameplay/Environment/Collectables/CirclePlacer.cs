using System.Linq;
using UnityEngine;

public class CirclePlacer : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;

    [ContextMenu("Replace children")]
    private void PlaceObjects()
    {
        var children = GetComponentsInChildren<Transform>();
        children = children.Where(c => c.gameObject != gameObject).ToArray();
        PlaceObjectsInCircle(children);
    }

    private void PlaceObjectsInCircle(Transform[] children)
    {
        var angleStep = 2 * Mathf.PI / children.Length;

        for (var i = 0; i < children.Length; i++)
        {
            var angle = i * angleStep;
            var x = Mathf.Cos(angle) * _radius;
            var z = Mathf.Sin(angle) * _radius;
            var position = new Vector3(x, 0, z) + transform.position;
            children[i].position = position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}