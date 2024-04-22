using UnityEngine;

public class FollowerFixedUpdate : Follower
{
    void Update()
    {
        Move(Time.fixedDeltaTime);
    }
}
