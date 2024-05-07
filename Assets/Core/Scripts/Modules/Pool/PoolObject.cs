using System.Collections;
using UnityEngine;

namespace PoolSystem.Main
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 2f;

        private void OnEnable()
        {
            if (_lifeTime > 0)
                StartCoroutine(nameof(LifeCoroutine));
        }

        private void OnDisable()
        {
            if (_lifeTime > 0)
                StopCoroutine(nameof(LifeCoroutine));
        }

        private IEnumerator LifeCoroutine()
        {
            yield return new WaitForSecondsRealtime(_lifeTime);
            Deactivate();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
