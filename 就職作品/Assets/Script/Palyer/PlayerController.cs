using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //--------------- �ϐ��錾 --------------------
    private Rigidbody _rigidbody;   //��������
    private Transform _transform;   //�ړ�����
    private Animator _animator;     //�A�j���[�V����
    private float _horizontal;      //����
    private float _vertical;        //�c��
    private Vector3 _velocity;      //�x�N�g����

    private float _speed = 3f;          //�X�s�[�h
    private float _topSpeed = 6f;       //�_�b�V���X�s�[�h

    private bool _runFlag;

    private Vector3 _aim;               //����
    private Quaternion _playerRotation; //��]

    float jumpForce = 600.0f;           //�W�����v��
    Vector3 playerPos;                  //���j�e�B�����̈ʒu������
    bool Ground = true;                 //�n�ʂɐڐG���Ă��邩�ۂ�
    int key = 0;

    public InputField inputField;
    public GameObject door;
    public GameObject fieldObject;

    //--------------- ���� --------------------
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();     //���������擾
        _transform = GetComponent<Transform>();     //�ړ������擾
        _animator = GetComponent<Animator>();       //�A�j���[�V�����擾

        _playerRotation = _transform.rotation;      //��]����

        _runFlag = false;                           //�_�b�V���p�t���O
        playerPos = transform.position;
    }

    //--------------- �X�V --------------------
    void FixedUpdate()
    {
        movePlayer();
        jumpPlayer();
    }

    //---------�ړ��֐�
    void movePlayer()
    {
        _animator.SetBool("walking", false);    //�A�j���[�V�������s���Ȃ�
        _animator.SetBool("running", false);

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                runFlag();
            }
            else
            {
                _speed = 3;
            }
            transform.position += transform.forward * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //�A�j���[�V�������s
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //�A�j���[�V�������s
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //�A�j���[�V�������s
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //�A�j���[�V�������s
        }




        #region
        //    _horizontal = Input.GetAxis("Horizontal");  //���ړ��{�^������
        //    _vertical = Input.GetAxis("Vertical");      //�c�ړ��{�^������

        //    var _horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);            //����]

        //    _velocity = _horizontalRotation * new Vector3(_horizontal, _rigidbody.velocity.y, _vertical).normalized;    //����]�ړ���

        //    _aim = _horizontalRotation * new Vector3(_horizontal, 0, _vertical).normalized;                             //��������
        //    if (_aim.magnitude > 0.5f)
        //    {
        //        _playerRotation = Quaternion.LookRotation(_aim, Vector3.up);    //��]���s
        //    }

        //    _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _playerRotation, 600 * Time.deltaTime); //�ړ����s

        //    if (_velocity.magnitude > 0.1f)
        //    {
        //        _animator.SetBool("walking", true);     //�A�j���[�V�������s
        //    }
        //    else
        //    {
        //        _animator.SetBool("walking", false);    //�A�j���[�V�����s���s
        //    }

        //    _rigidbody.velocity = _velocity * _speed;   //�ړ��ʌ���
        #endregion

    }

    void runFlag()
    {
        _runFlag = true;
        _speed = _topSpeed;
        _animator.SetBool("running", true);
    }

    //---------�W�����v�֐�
    void jumpPlayer()
    {
        if (Ground)
        {
            if (Input.GetButton("Jump"))
            {
                //jumpForce�̕���������ɗ͂�������
                _rigidbody.AddForce(transform.up * jumpForce);
                Ground = false;
            }
        }
    }

    //�W�����v��APlane�ɐڐG�������ɐڐG�����true�ɖ߂�
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!Ground)
                Ground = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Door_snaps")
            fieldObject.SetActive(true);
    }

    public void InputPass()
    {
        if (inputField.text == "4452")
            door.GetComponent<ClearDoorOpen>().DoorMove();
        fieldObject.SetActive(false);

    }
}

