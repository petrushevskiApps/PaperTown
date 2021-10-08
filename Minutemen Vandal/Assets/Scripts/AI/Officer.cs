using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Officer : Pedestrian
{
    public Transform player;

    protected override float GetMovementTimeout()
    {
        if (player == null)
        {
            return base.GetMovementTimeout();
        }
        else
        {
            return 0;
        }
    }

    protected override Vector3 GetDestination()
    {
        if (player == null)
        {
            return base.GetDestination();
        }
        else
        {
            return player.position;
        }
    }
}
