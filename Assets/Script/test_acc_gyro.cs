using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_acc_gyro : MonoBehaviour
{
    float speed = 10.0f;
    float angularSpeed = 360.0f;

    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;

        transform.Translate(dir * speed, Space.World);
        transform.Rotate(new Vector3(dir.z, 0, -dir.x) * angularSpeed, Space.World);
    }
}