using UnityEngine;

public class FollowerLateUpdate : Follower
{
    private void LateUpdate()
    {
        Move(Time.deltaTime);
    }
}
