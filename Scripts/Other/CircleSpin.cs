using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpin : MonoBehaviour
{
    private Vector3 _currentEulerAngles;
    private Vector3 _vectorRotate = new Vector3(0, 20, 0);

    void Update()
    {
        Spin();
    }

    private void Spin()
    {
        _currentEulerAngles += _vectorRotate * Time.deltaTime;
        transform.localEulerAngles = _currentEulerAngles;
    }
}
