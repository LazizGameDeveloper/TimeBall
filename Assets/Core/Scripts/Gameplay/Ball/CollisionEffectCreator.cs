using UnityEngine;

public class CollisionEffectCreator : MonoBehaviour
{
    [SerializeField] private CollisionEffectPool _poolEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            _poolEffect.GetFromPool(transform.position);
            SoundManager.Instance.PlayFX(AllSfxSounds.FX_1, collision.transform.position);
        }
    }
}
