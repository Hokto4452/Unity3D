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


    public float sight_x = 0;
    public float sight_y = 0;
    private bool isFront;
    private bool isBack;
    private bool isLeft;
    private bool isRight;
    public float moveSpd;

    //float x = Input.GetAxis("Horizontal");
    //float z = Input.GetAxis("Vertical");
    //float angleH = Input.GetAxis("Horizontal");
    //float angleV = Input.GetAxis("Vertical");

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
        //DebugLog();
        //movePlayer();
        jumpPlayer();
        ProCon3rd();
        ProConMove();
       
    }

    void DebugLog()
    {
        #region
        //L Stick
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        //R Stick
        float rsh = Input.GetAxis("R_Stick_H");
        float rsv = Input.GetAxis("R_Stick_V");
        //D-Pad
        float dph = Input.GetAxis("D_Pad_H");
        float dpv = Input.GetAxis("D_Pad_V");
        //Trigger
        float tri = Input.GetAxis("L_R_Trigger");

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("joystick button 0"))
        {
            Debug.Log("A");
        }   //A
        if (Input.GetKeyDown("joystick button 1"))
        {
            Debug.Log("B");
        }   //B
        if (Input.GetKeyDown("joystick button 2"))
        {
            Debug.Log("Y");
        }   //Y
        if (Input.GetKeyDown("joystick button 3"))
        {
            Debug.Log("X");
        }   //X
        if (Input.GetKeyDown("joystick button 4"))
        {
            Debug.Log("L");
        }   //L
        if (Input.GetKeyDown("joystick button 5"))
        {
            Debug.Log("R");
        }   //R
        if (Input.GetKeyDown("joystick button 6"))
        {
            Debug.Log("ZL");
        }   //ZL
        if (Input.GetKeyDown("joystick button 7"))
        {
            Debug.Log("ZR");
        }   //ZR
        if (Input.GetKeyDown("joystick button 8"))
        {
            Debug.Log("-");
        }   //-
        if (Input.GetKeyDown("joystick button 9"))
        {
            Debug.Log("+");
        }   //+
        if ((hori != 0) || (vert != 0))           
        {
            // Debug.Log("stick:" + hori + "," + vert);
        }              //Stick
        if ((lsh != 0) || (lsv != 0))
        {
           //Debug.Log("L stick:" + lsh + "," + lsv);   
        }                //L Stick
        if ((rsh != 0) || (rsv != 0))
        {
            //Debug.Log("R Stick:" + rsh + "," + rsv);
        }                //R Stick
        if ((dph != 0) || (dpv != 0)) 
        {
            //Debug.Log("HV �\���L�[:" + dph + "," + dpv);
        }                //HV �\���L�[
        if (tri > 0)
        {
            //Debug.Log("dddd:" + tri);
        }
        else if (tri < 0)
        {
            //Debug.Log("eee:" + tri);
        }
        else
        {
            //.Log("  trigger:none");
        }
        #endregion

    }

    public void ProCon3rd()
    {
        float angleH = Input.GetAxis("R_Stick_H");
        float angleV = Input.GetAxis("R_Stick_V");

        if (sight_y > 80)
        {
            if (angleV < 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else if (sight_y < -90)
        {
            if (angleV > 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else
        {
            sight_y = sight_y + angleV;
        }

        if (sight_x >= 360)    //sight_x ��360�x�𒴂����360�������A���������̒[����sight_x�Ɏc��
        {
            sight_x = sight_x - 360;
        }
        else if (sight_x < 0)  //sight_x ��0�x��������360����sight_x�������A�c��������sight_x�Ɏc��
        {
            sight_x = 360 - sight_x;
        }
        sight_x = sight_x + angleH;
        transform.localRotation = Quaternion.Euler(sight_y, sight_x, 0);
    }

    void ProConMove()
    {
        float moveH = Input.GetAxis("L_Stick_H");
        float moveV = Input.GetAxis("L_Stick_V");

        _animator.SetBool("walking", false);    //�A�j���[�V�������s���Ȃ�
        _animator.SetBool("running", false);    //�A�j���[�V�������s���Ȃ�

        //--------------------------------------
        //'''''�L�����N�^�[�ړ�'''''
        //--------------------------------------
        isFront = moveV > 0;
        isBack = moveV < 0;
        isLeft = moveH < 0;
        isRight = moveH > 0;
        
        //--------------------------------------
        //'''''�X�s�[�h����'''''
        //--------------------------------------
        moveSpd = 3f * (Mathf.Abs(moveV) + Mathf.Abs(moveH));

        if (moveSpd > 4f)          //�X�s�[�h����
        {
            moveSpd = 3.9f;
        }
        else if (moveSpd <= 4f)
        {
            moveSpd = 3f * (Mathf.Abs(moveV) + Mathf.Abs(moveH));
        }
        //Vector3 v = new Vector3(0f, 0f, 0f);

        if (isFront)
        {

            transform.position += transform.forward * moveSpd * Time.deltaTime;
            if (moveSpd >= 3)
            {
                runFlag();
            }
            _animator.SetBool("walking", true);     //�A�j���[�V�������s

        }
        if (isBack)
        {
            transform.position -= transform.forward * moveSpd * Time.deltaTime;
            _animator.SetBool("walking", true);     //�A�j���[�V�������s
        }
        if (isLeft)
        {
            transform.position -= transform.right * moveSpd * Time.deltaTime;
            _animator.SetBool("walking", true);     //�A�j���[�V�������s
        }
        if (isRight)
        {
            transform.position += transform.right * moveSpd * Time.deltaTime;
            _animator.SetBool("walking", true);     //�A�j���[�V�������s
        }
        
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

