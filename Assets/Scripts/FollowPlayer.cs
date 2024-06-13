using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public Transform Player;
    [SerializeField] public float xDiff;

    Vector3 localRotation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x + xDiff, transform.position.y, transform.position.z);
    }
}
