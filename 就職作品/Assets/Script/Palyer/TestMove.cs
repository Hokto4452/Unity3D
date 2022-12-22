using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    //Rigidbody��ϐ��ɓ����
    Rigidbody rb;
    //�ړ��X�s�[�h
    float speed = 3.0f;
    //�W�����v��
    float jumpForce = 850.0f;
    //���j�e�B�����̈ʒu������
    Vector3 playerPos;
    //�n�ʂɐڐG���Ă��邩�ۂ�
    bool Ground = true;
    int key = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody���擾
        rb = GetComponent<Rigidbody>();
        //���j�e�B�����̌��݂�菭���O�̈ʒu��ۑ�
        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputKey();
        Move();
    }

    void GetInputKey()
    {
        //A�ED�L�[�A�����L�[�ŉ��ړ�
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        //W�ES�L�[�A�����L�[�őO��ړ�
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }
    }
    void Move()
    {
        if (Ground)
        {
            if (Input.GetButton("Jump"))
            {
                //jumpForce�̕���������ɗ͂�������
                rb.AddForce(transform.up * jumpForce);
                Ground = false;
            }

        }

        //���݂̈ʒu�{���͂������l�̏ꏊ�Ɉړ�����
        rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

        //���j�e�B�����̍ŐV�̈ʒu���班���O�̈ʒu�������ĕ���������o��
        Vector3 direction = transform.position - playerPos;

        //�ړ������������ł��������ꍇ�ɕ����]��
        if (direction.magnitude >= 0.01f)
        {
            //direction��X����Z���̕�������������
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        else
        {
            key = 0;
        }

        //���j�e�B�����̈ʒu���X�V����
        playerPos = transform.position;

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






    ////Rigidbody��ϐ��ɓ����
    //Rigidbody rb;
    ////�ړ��X�s�[�h
    //float speed = 3.0f;
    ////�W�����v��
    //float jumpForce = 400.0f;
    ////Animator������ϐ�
    //private Animator animator;
    ////���j�e�B�����̈ʒu������
    //Vector3 playerPos;
    ////�n�ʂɐڐG���Ă��邩�ۂ�
    //bool Ground = true;
    //int key = 0;

    //string state;
    //string prevState;


    //void Start()
    //{
    //    //Rigidbody���擾
    //    rb = GetComponent<Rigidbody>();
    //    //���j�e�B������Animator�ɃA�N�Z�X����
    //    animator = GetComponent<Animator>();
    //    //���j�e�B�����̌��݂�菭���O�̈ʒu��ۑ�
    //    playerPos = transform.position;
    //}

    //void Update()
    //{
    //    GetInputKey();
    //    ChangeState();
    //    ChangeAnimation();
    //    Move();
    //}


    //void GetInputKey()
    //{
    //    //A�ED�L�[�A�����L�[�ŉ��ړ�
    //    float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

    //    //W�ES�L�[�A�����L�[�őO��ړ�
    //    float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

    //    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
    //    {
    //        key = 1;
    //    }

    //    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        key = -1;
    //    }


    //}

    //void ChangeState()
    //{
    //    if (Ground)
    //    {
    //        if (key != 0)
    //        {
    //            state = "RUN";
    //        }
    //        else
    //        {
    //            state = "IDLE";
    //        }
    //    }
    //    else
    //    {
    //        state = "JUMP";
    //    }
    //}

    //void ChangeAnimation()
    //{
    //    if (prevState != state)
    //    {
    //        switch (state)
    //        {
    //            case "JUMP":
    //                animator.SetBool("Jumping", true);
    //                animator.SetBool("Running", false);
    //                animator.SetBool("Idle", false);
    //                break;
    //            case "RUN":
    //                animator.SetBool("Jumping", false);
    //                animator.SetBool("Running", true);
    //                animator.SetBool("Idle", false);
    //                break;
    //            default:
    //                animator.SetBool("Jumping", false);
    //                animator.SetBool("Running", false);
    //                animator.SetBool("Idle", true);
    //                break;
    //        }
    //        prevState = state;
    //    }
    //}

    //void Move()
    //{
    //    if (Ground)
    //    {
    //        if (Input.GetButton("Jump"))
    //        {
    //            //jumpForce�̕���������ɗ͂�������
    //            rb.AddForce(transform.up * jumpForce);
    //            Ground = false;
    //        }

    //    }

    //    //���݂̈ʒu�{���͂������l�̏ꏊ�Ɉړ�����
    //    rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

    //    //���j�e�B�����̍ŐV�̈ʒu���班���O�̈ʒu�������ĕ���������o��
    //    Vector3 direction = transform.position - playerPos;

    //    //�ړ������������ł��������ꍇ�ɕ����]��
    //    if (direction.magnitude >= 0.01f)
    //    {
    //        //direction��X����Z���̕�������������
    //        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    }
    //    else
    //    {
    //        key = 0;
    //    }

    //    //���j�e�B�����̈ʒu���X�V����
    //    playerPos = transform.position;

    //}

    ////�W�����v��APlane�ɐڐG�������ɐڐG�����true�ɖ߂�
    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.tag == "Ground")
    //    {
    //        if (!Ground)
    //            Ground = true;
    //    }
    //}
}
