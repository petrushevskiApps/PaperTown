using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    void Update()
    {
        if (_player.gameObject)
        {
            transform.position = _player.transform.position;
        }
    }
}
