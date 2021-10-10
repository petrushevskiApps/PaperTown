using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMoveCamera : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<FPSPlayerMovement>().transform;
    }
    void Update()
    {
        transform.position = _player.transform.position;
    }
}
