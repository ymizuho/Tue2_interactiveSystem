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

    private Vector3 _initialPosition; // �����ʒu
    private Quaternion _initialRotation; // ������]

    // Start is called before the first frame update
    void Start()
    {
        // Molkky = GameObject.Find("Molkky");
        // �W���C���X�R�[�v��L����
        Input.gyro.enabled = true;

        // �����ʒu�E������]�̎擾
        _initialPosition = Molkky.gameObject.transform.position;
        _initialRotation = Molkky.gameObject.transform.rotation;

        // ThrowBtn�������ꂽ��
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

        // �����ɔ�΂�
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

        Molkky.gameObject.transform.position = _initialPosition; // �ʒu�̏�����
        Molkky.gameObject.transform.rotation = _initialRotation; // ��]�̏�����
        rb.velocity = Vector3.zero;         // ���`���x���[����
        rb.angularVelocity = Vector3.zero;  // �p���x���[����
    }
}
