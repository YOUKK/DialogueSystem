using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 delta = new Vector3(0.0f, 4.0f, -10.0f);
    private GameObject player;
    private 

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = player.transform.position + delta;
        transform.LookAt(player.transform);
    }
}
