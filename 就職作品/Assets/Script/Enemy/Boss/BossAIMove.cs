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
        EruptionAttack,
        BackJump,
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

    private eruptionAttack eruptionAttackFlag;
    private float eruptionCountTime;
    [SerializeField] private float beforeEruptionWait = 1f;
    public int eruptionBulletCount = 1;
    private float eruptionBulletInterval;
    private float eruptionReloadInterval;
    public bool notEruptionBullt;
    bool haveEruptionBullet;
    public GameObject posEruptionBullet;
    public Rigidbody eruptionBullet;

    private backJump backJumpFlag;

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

        eruptionAttackFlag = GetComponent<eruptionAttack>();
        eruptionCountTime = 0f;
        notEruptionBullt = false;
        haveEruptionBullet = false;

        backJumpFlag = GetComponent<backJump>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(eruptionBulletInterval);
        //�@�����܂��̓L�����N�^�[��ǂ���������
        if (state == BossState.Walk || state == BossState.Chase)
        {
            //�@�L�����N�^�[��ǂ��������Ԃł���΃L�����N�^�[�̖ړI�n���Đݒ�
            if (state == BossState.Chase)
            {
                //Debug.Log("�ǐ�");
                //Debug.Log(eruptionAttackFlag);
                setPosition.SetDestination(playerTransform.position);
                //SetState(BossState.EruptionAttack);
            }
            #region
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
            #endregion
            //�@�L�����N�^�[���n�ʂɂ�����
            if (_bossController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }
            //�@������
            if (state == BossState.Walk)
            {
                //Debug.Log("����");
                walkTime += Time.deltaTime;
                //5�b�ȏ㏄�񂵂���X�e�[�g��ύX
                if (walkTime > 5)
                {
                    //Debug.Log("���񐧌�����");
                    SetState(BossState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
                //�@�ړI�n�ɓ����������ǂ����̔���
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 2f)
                {
                    //Debug.Log("����");
                    SetState(BossState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
            }
            //�@�ǐՏ��
            else if (state == BossState.Chase)
            {
                //�@�U�����鋗����������U��
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1f)
                {
                    //Debug.Log("�U��");
                    SetState(BossState.Attack);
                }
            }
            else if (eruptionAttackFlag == true) 
            {
                //Debug.Log("���΍U��");
                SetState(BossState.EruptionAttack);
            }
            else if (backJumpFlag == true)
            {
                //Debug.Log("���W�����v");
                SetState(BossState.BackJump);
            }
        }
        // �������Ă������莞�ԑ҂�
        else if (state == BossState.Wait)
        {
            //Debug.Log("��~");
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
            //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
            if (elapsedTime > waitTime)
            {
                //Debug.Log("������ĊJ");
                SetState(BossState.Walk);
            }
        }
        else if (state == BossState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > freezeTime)
            {
                //Debug.Log("������ĊJ����");
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
        else if(tempState == BossState.EruptionAttack)
        {
            velocity = Vector3.zero;
            Rigidbody clone;
            if(eruptionBulletCount == 0)
            {
                haveEruptionBullet = true;
            }

            eruptionReloadInterval += Time.deltaTime;
            if(eruptionReloadInterval > 3)
            {
                eruptionBulletInterval += Time.deltaTime;
                if(eruptionBulletCount > 0)
                {
                    eruptionBulletCount -= 1;
                    notEruptionBullt = true;
                    clone = Instantiate(eruptionBullet, posEruptionBullet.transform.position, transform.rotation) as Rigidbody;
                    clone.velocity = transform.TransformDirection(Vector3.up * 3f);
                }
            }
            Debug.Log("���΍U��");
        }
        else if(tempState == BossState.BackJump)
        {
            //���Ɉړ�velocity�ƃW�����v�𓯎��ɍs��


            Debug.Log("���W�����v");
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
