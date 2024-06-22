using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerINTRO : MonoBehaviour
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
        if (Player.position.x < 8.1f && Player.position.x > -2.2f)
        {
            transform.position = new Vector3(Player.position.x + xDiff, transform.position.y, transform.position.z);
        }
    }
}
