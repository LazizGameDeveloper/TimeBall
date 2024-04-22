using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BallController))]
public class RandomVelocityAdder : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private float _slowmotionDuration = 0.1f;
     
    private BallController _ballController;

    private void Awake()
    {
        _ballController = GetComponent<BallController>();
    }

    private void Start()
    {
        StartCoroutine(AddRandomVelocity(-1, 1));
    }

    private IEnumerator AddRandomVelocity(float minValue, float maxValue)
    {
        while (true)
        {
            var rX = Random.Range(minValue, maxValue);
            var rZ = Random.Range(minValue, maxValue);

            _timeManager.DoSlowmotion();
            yield return new WaitForSeconds(_slowmotionDuration);
            _timeManager.UndoSlowmotion();

            var direction = new Vector3(rX, 0f, rZ);
            _ballController.Move(direction);
            yield return new WaitForSeconds(3f);
        }
    }
}
