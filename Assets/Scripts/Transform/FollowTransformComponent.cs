using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follows a transform
/// Used when you dont want to be a child of the gameobject to follow
/// </summary>
public class FollowTransformComponent : MonoBehaviour {

    public Transform otherTransform;

	void Update () {
        transform.position = otherTransform.transform.position;
    }
}
