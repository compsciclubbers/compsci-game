// External release version 2.0.0

using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Custom character controller, to be used by attaching the component to an object
/// and writing scripts attached to the same object that recieve the "SuperUpdate" message
/// </summary>
public class CharacterController : MonoBehaviour
{

    private readonly int kPoseBufferSize = 2000;
    private readonly Vector3 kCollisionVector = new Vector3(-1, -1, -1);

    private Queue<TimestampedVector3> mPoseHistory = new Queue<TimestampedVector3>();
    private Boolean mIsColliding = false;

    private void FixedUpdate()
    {
        mPoseHistory.Enqueue(TimestampedVector3.CreateTimestampedVector3(this));
        if(mPoseHistory.Count > kPoseBufferSize)
        {
            mPoseHistory.Dequeue();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        mIsColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        mIsColliding = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        this.transform.position = mPoseHistory.Dequeue().GetVector3();
    }

    public Boolean IsColliding()
    {
        return mIsColliding;
    }

}