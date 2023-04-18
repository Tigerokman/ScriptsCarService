using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformCamera : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void FixedUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        //transform.rotation = new Quaternion(90,0,0,0);
    }
}
