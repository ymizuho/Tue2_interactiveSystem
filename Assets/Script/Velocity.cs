using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button SetBtn, ResetBtn, ReleaseBtn;
    [SerializeField] UnityEngine.UI.Scrollbar PositionBar, DirectionBar;
    float Acceleration_x = 1;
    float Acceleration_y = 1;
    float Acceleration_z = 1;

    float speed = 10.0f;
    float angularSpeed = 360.0f;

    public GameObject Molkky, Direction;

    private Vector3 _initialPosition_Molkky, _initialPosition_Direction; // �����ʒu
    private Quaternion _initialRotation_Molkky, _initialRotation_Direction; // ������]

    private Vector3 accumulatedAcceleration = Vector3.zero;
    private bool isButtonPressed = false;

    public float moveRange = 15f; // �I�u�W�F�N�g�̈ړ��͈�
    public float rotationRange = 120f; // ���̉�]�͈́i�x���j

    private float initialObjectPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Molkky = GameObject.Find("Molkky");
        // �W���C���X�R�[�v��L����
        Input.gyro.enabled = true;

        Time.timeScale = 2.0f; // �ʏ��2�{�̑��x

        // �����ʒu�E������]�̎擾
        _initialPosition_Molkky = Molkky.gameObject.transform.position;
        _initialRotation_Molkky = Molkky.gameObject.transform.rotation;
        // �����ʒu�E������]�̎擾
        _initialPosition_Direction = Direction.gameObject.transform.position;
        _initialRotation_Direction = Direction.gameObject.transform.rotation;

        // �{�^���̃C�x���g���X�i�[��ݒ�
        SetBtn.onClick.AddListener(Set);
        ReleaseBtn.onClick.AddListener(Release);
        ResetBtn.onClick.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonPressed)
        {
            Debug.Log("obtaining");
            // �����x�Z���T�̒l��ϕ�
            accumulatedAcceleration += (Input.acceleration - Input.gyro.gravity) * Time.deltaTime;
        }

        // �X�N���[���o�[�̒l�Ɋ�Â��ăI�u�W�F�N�g���ړ�
        float newPositionX = initialObjectPosition + (PositionBar.value - 0.5f) * moveRange;
        Molkky.gameObject.transform.position = new Vector3(newPositionX, Molkky.gameObject.transform.position.y, Molkky.gameObject.transform.position.z);
        Direction.gameObject.transform.position = new Vector3(newPositionX, Direction.gameObject.transform.position.y, Direction.gameObject.transform.position.z);

        // �X�N���[���o�[�̒l�Ɋ�Â��ăI�u�W�F�N�g���ړ�
        float rotationAngle = (DirectionBar.value - 0.5f) * rotationRange;
        Direction.gameObject.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }

    void Set()
    {
        accumulatedAcceleration = Vector3.zero; // �ϕ��l�����Z�b�g
        isButtonPressed = true; // �ϕ��J�n

        SetBtn.gameObject.SetActive(false);
        ReleaseBtn.gameObject.SetActive(true);

        /*
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
        */

    }

    void Release()
    {
        isButtonPressed = false; // �ϕ���~
        Debug.Log("Integrated Acceleration: " + accumulatedAcceleration); // �ϕ��l��\��

        Vector3 dir = Vector3.zero;

        dir.x = -accumulatedAcceleration.x;
        dir.y = -accumulatedAcceleration.z;
        dir.z = -accumulatedAcceleration.y;
        // Debug.Log("Input.gyro.gravity:" + Input.gyro.gravity);
        Debug.Log("accelerationWithoutGravity.x:" + accumulatedAcceleration.x);
        Debug.Log("accelerationWithoutGravity.y:" + accumulatedAcceleration.y);
        Debug.Log("accelerationWithoutGravity.z:" + accumulatedAcceleration.z);

        // �����ɔ�΂�
        Rigidbody rb = Molkky.GetComponent<Rigidbody>();
        Vector3 force = new Vector3(dir.x * 30, dir.y * 50, dir.z * 200);
        rb.AddForce(force, ForceMode.Impulse);

        SetBtn.gameObject.SetActive(true);
        ReleaseBtn.gameObject.SetActive(false);
    }

    void Reset()
    {
        Rigidbody rb_Molkky = Molkky.GetComponent<Rigidbody>();
        Rigidbody rb_Direction = Direction.GetComponent<Rigidbody>();

        Molkky.gameObject.transform.position = _initialPosition_Molkky; // �ʒu�̏�����
        Molkky.gameObject.transform.rotation = _initialRotation_Molkky; // ��]�̏�����
        Direction.gameObject.transform.position = _initialPosition_Direction; // �ʒu�̏�����
        Direction.gameObject.transform.rotation = _initialRotation_Direction; // ��]�̏�����
        rb_Molkky.velocity = Vector3.zero;         // ���`���x���[����
        rb_Direction.angularVelocity = Vector3.zero;  // �p���x���[����
    }
}
