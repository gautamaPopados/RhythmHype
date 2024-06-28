using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerINTRO : MonoBehaviour
{
    [SerializeField] public Transform Player;
    [SerializeField] public float xDiff;
    public float rightBorder = 8.1f;
    public float leftBorder = -2.2f;
    Vector3 localRotation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.x < rightBorder && Player.position.x > leftBorder)
        {
            transform.position = new Vector3(Player.position.x + xDiff, transform.position.y, transform.position.z);
        }
    }
}
