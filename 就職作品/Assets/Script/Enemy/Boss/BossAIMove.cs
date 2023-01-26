using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIMove : MonoBehaviour
{
    public enum BossState
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    };

    private CharacterController _bossController;    //
    private Animator animator;                      //
    //�@�ړI�n
    private Vector3 destination;
    //�@�����X�s�[�h
    [SerializeField] private float walkSpeed = 1.0f;//
    //�@���x
    private Vector3 velocity;                       //
    //�@�ړ�����
    private Vector3 direction;                      //
    //�@�����t���O
    private bool arrived;
    //  �X�N���v�g
    private MovePosition setPosition;
    //�@�҂�����
    [SerializeField] private float waitTime = 5f;
    //�@�o�ߎ���
    private float elapsedTime;
    // �G�̏��
    private BossState state;
    //�@�v���C���[Transform
    private Transform playerTransform;

    private float moveTime;
    [SerializeField] private float limitmoveTime = 5f;
    private float walkTime;

    [SerializeField] float freezeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        setPosition = GetComponent<MovePosition>();
        setPosition.CreateRandomPosition();
        _bossController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(BossState.Walk);
        moveTime = 0f;
        walkTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //�@�����܂��̓L�����N�^�[��ǂ���������
        if (state == BossState.Walk || state == BossState.Chase)
        {
            //if (state == BossState.Walk)
            //{
            //    Debug.Log("����");
            //    moveTime += Time.deltaTime;
            //    Debug.Log(moveTime);
            //    if (moveTime >= limitmoveTime)
            //    {
            //        moveTime = 0f;
            //        SetState(BossState.WalkOut);
            //    }
            //}
            //if(state ==BossState.WalkOut)
            //{
            //    Debug.Log("�s���������Ԓ�");
            //    moveTime += Time.deltaTime;
            //    Debug.Log(moveTime);
            //}
            //�@�L�����N�^�[��ǂ��������Ԃł���΃L�����N�^�[�̖ړI�n���Đݒ�
            if (state == BossState.Chase)
            {
                Debug.Log("�ǐ�");
                setPosition.SetDestination(playerTransform.position);

            }
            //if (state == BossState.Wait)
            //{
            //    Debug.Log("�s����������");
            //    elapsedTime += Time.deltaTime;
            //    Debug.Log(elapsedTime);
            //    if (elapsedTime >= waitTime)
            //    {
            //        Debug.Log("������ĊJ");
            //        SetState(BossState.Walk);
            //    }
            //}
            //�@�L�����N�^�[���n�ʂɂ�����
            if (_bossController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }
            if (state == BossState.Walk)
            {
                Debug.Log("����");
                walkTime += Time.deltaTime;
                if (walkTime > 5)
                {
                    Debug.Log("���񐧌�����");
                    SetState(BossState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
                //�@�ړI�n�ɓ����������ǂ����̔���
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 2f)
                {
                    Debug.Log("����");
                    SetState(BossState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
            }
            else if (state == BossState.Chase)
            {
                //�@�U�����鋗����������U��
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1f)
                {
                    Debug.Log("�U��");
                    SetState(BossState.Attack);
                }
            }
        }
        // �������Ă������莞�ԑ҂�
        else if (state == BossState.Wait)
        {
            Debug.Log("��~");
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
            if (elapsedTime > waitTime)
            {
                Debug.Log("������ĊJ");
                SetState(BossState.Walk);
            }
        }
        else if (state == BossState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > freezeTime)
            {
                Debug.Log("������ĊJ����");
                SetState(BossState.Walk);
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        _bossController.Move(velocity * Time.deltaTime);
    }
    //�@�G�L�����N�^�[�̏�ԕύX���\�b�h
    public void SetState(BossState tempState, Transform targetObj = null)
    {
        if (tempState == BossState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            state = tempState;
            setPosition.CreateRandomPosition();
            //animator.SetFloat("Speed", 1f);
        }
        //else if(tempState == BossState.WalkOut)
        //{
        //    //elapsedTime = 0f;
        //    //moveTime = 0f;
        //    state = tempState;
        //    arrived = true;
        //    velocity = Vector3.zero;
        //    animator.SetFloat("Speed", 0f);
        //}
        else if (tempState == BossState.Chase)
        {
            state = tempState;
            //�@�ҋ@��Ԃ���ǂ�������ꍇ������̂�Off
            arrived = false;
            //�@�ǂ�������Ώۂ��Z�b�g
            playerTransform = targetObj;
        }
        else if (tempState == BossState.Wait)
        {
            elapsedTime = 0f;
            walkTime = 0f;
            state = tempState;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
        else if (tempState == BossState.Attack)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", true);
        }
        else if (tempState == BossState.Freeze)
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", false);
        }
    }

    //�@�G�L�����N�^�[�̏�Ԏ擾���\�b�h
    public BossState GetState()
    {
        return state;
    }
}
