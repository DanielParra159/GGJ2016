using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 7.5f, 0f);

    public bool ignoreY =false;

    private void LateUpdate()
    {
        if (ignoreY)
        {
            transform.position = new Vector3(target.position.x, 0, target.position.z) + offset;
        }
        else
        {
            transform.position = target.position + offset;
        }
    }
}