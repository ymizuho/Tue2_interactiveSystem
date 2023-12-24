using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button ThrowBtn, ResetBtn;
    float Acceleration_x = 1;
    float Acceleration_y = 1;
    float Acceleration_z = 1;

    float speed = 10.0f;
    float angularSpeed = 360.0f;

    public GameObject Molkky;

    private Vector3 _initialPosition; // 初期位置
    private Quaternion _initialRotation; // 初期回転

    // Start is called before the first frame update
    void Start()
    {
        // Molkky = GameObject.Find("Molkky");
        // ジャイロスコープを有効化
        Input.gyro.enabled = true;

        // 初期位置・初期回転の取得
        _initialPosition = Molkky.gameObject.transform.position;
        _initialRotation = Molkky.gameObject.transform.rotation;

        // ThrowBtnが押された時
        ThrowBtn.onClick.AddListener(Throw);
        ResetBtn.onClick.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Throw()
    {
        Vector3 dir = Vector3.zero;
        Vector3 accelerationWithoutGravity = Input.acceleration - Input.gyro.gravity;

        dir.x = -accelerationWithoutGravity.x;
        dir.y = -accelerationWithoutGravity.z;
        dir.z = -accelerationWithoutGravity.y;
        Debug.Log("Input.gyro.gravity:" + Input.gyro.gravity);
        Debug.Log("accelerationWithoutGravity.x:" + accelerationWithoutGravity.x);
        Debug.Log("accelerationWithoutGravity.y:" + accelerationWithoutGravity.y);
        Debug.Log("accelerationWithoutGravity.z:" + accelerationWithoutGravity.z);

        // 遠くに飛ばす
        Rigidbody rb = Molkky.GetComponent<Rigidbody>();
        Vector3 force = new Vector3(dir.x * 10, dir.y * 10, dir.z * 10);
        rb.AddForce(force, ForceMode.Impulse);

        /*
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;

        transform.Translate(dir * speed, Space.World);
        transform.Rotate(new Vector3(dir.z, 0, -dir.x) * angularSpeed, Space.World);
        */

    }

    void Reset()
    {
        Rigidbody rb = Molkky.GetComponent<Rigidbody>();

        Molkky.gameObject.transform.position = _initialPosition; // 位置の初期化
        Molkky.gameObject.transform.rotation = _initialRotation; // 回転の初期化
        rb.velocity = Vector3.zero;         // 線形速度をゼロに
        rb.angularVelocity = Vector3.zero;  // 角速度もゼロに
    }
}
