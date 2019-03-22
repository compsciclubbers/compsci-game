using System;
using UnityEngine;

public class TimestampedVector3
{

    private readonly Vector3 mVector3;
    private readonly Double mTimestamp;

	public TimestampedVector3(Vector3 pVector3, Double pTimestamp)
    {
        mVector3 = pVector3;
        mTimestamp = pTimestamp;
    }

    public Vector3 GetVector3()
    {
        return mVector3;
    }

    public Double GetTimestamp()
    {
        return mTimestamp;
    }

    public static TimestampedVector3 CreateTimestampedVector3(Vector3 pVector3)
    {
        return new TimestampedVector3(pVector3, Time.time);
    }

    public static TimestampedVector3 CreateTimestampedVector3(MonoBehaviour pBehaviorObject)
    {
        return new TimestampedVector3(pBehaviorObject.transform.position, Time.time);
    }

}
