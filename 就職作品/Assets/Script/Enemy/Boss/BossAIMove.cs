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
        Splash,
        Freeze
    };
    public Vector3 bossPos = new Vector3();
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
    private Rigidbody bacaJump;
    public float jumpForce = 600.0f;
    public float backSpeed = 5f;
    public float gravity = 20.0f;
    private float jumpInterval;
    private float jumpReloadInterval;
    public GameObject player;
    bool haveJump;

    private splashAttack splashFlag;
    private splashAttack splashBulletPos;
    [SerializeField]GameObject splashPos;
    private float splashCountTime;
    [SerializeField] private float beforeSplashWait = 1f;
    public int splashCount = 12;
    public Vector3[] pos = new Vector3[12];
    public GameObject[] muzzle = new GameObject[12];
    private float splashInterval;
    private float splashReloadinterval;
    public bool notSplash;
    bool haveSplash;
    public GameObject posSplash;
    //public Rigidbody splash;
    public GameObject splash;
    public GameObject splashbuullet;

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
        //bacaJump = GetComponent<CharacterController>();
        haveJump = true;

        splashFlag = GetComponent<splashAttack>();
        splashBulletPos.GetComponent<splashAttack>();
        splashPos.GetComponent<splashAttack>();

        
        splashCountTime = 0f;
        notSplash = false;
        haveSplash = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(splashInterval);
        //�@�����܂��̓L�����N�^�[��ǂ���������
        if (state == BossState.Walk || state == BossState.Chase)
        {
            //�@�L�����N�^�[��ǂ��������Ԃł���΃L�����N�^�[�̖ړI�n���Đݒ�
            if (state == BossState.Chase)
            {
                //Debug.Log("�ǐ�");
                //Debug.Log(eruptionAttackFlag);
                setPosition.SetDestination(playerTransform.position);
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
                velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
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
            else if (splashFlag == true)
            {
                SetState(BossState.Splash);
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
                    GameObject sp = Instantiate(splashbuullet, posEruptionBullet.transform.position, transform.rotation);
                    Destroy(sp, 3.0f);
                    clone = Instantiate(eruptionBullet, posEruptionBullet.transform.position, transform.rotation) as Rigidbody;
                    clone.velocity = transform.TransformDirection(Vector3.up * 3f);
                }
            }
            //Debug.Log("���΍U��");
        }
        else if(tempState == BossState.BackJump)
        {
            //���Ɉړ��ƃW�����v�𓯎��ɍs��
            if (haveJump == true)
            {
                //jumpForce�̕���������ɗ͂�������
                //_bossController.AddForce(transform.up * jumpForce);
                velocity.y = jumpForce;
            }
            else if (haveJump == false)
            {
                //_bossController.AddForce(-transform.forward * backSpeed);
                velocity.x = -backSpeed;
            }
            jumpReloadInterval += Time.deltaTime;
            if (jumpReloadInterval < 3)
            {
                haveJump = false;
            }
            else if (jumpReloadInterval > 3)
            {
                haveJump = true;
                jumpReloadInterval = 0f;
            }
            velocity.y = velocity.y - (gravity * Time.deltaTime);
            _bossController.Move(velocity * Time.deltaTime);

            //Debug.Log("���W�����v");
        }
        else if (tempState == BossState.Splash)
        {
            //pos = splashPos.GetComponent<splashAttack>().pushPos;
            muzzle = splashPos.GetComponent<splashAttack>().muzzle;
            velocity = Vector3.zero;
            Rigidbody[] splashBullet = new Rigidbody[12];
            if(splashCount == 0)
            {
                haveSplash = true;
            }
            splashInterval += Time.deltaTime;
            if (splashInterval >= 3)
            {
                if (notSplash == false)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        //splash.transform.position = pos[i];
                        //splash.gameObject.AddComponent<Rigidbody>();
                        GameObject sp = Instantiate(splash, muzzle[i].transform.position, muzzle[i].transform.rotation);
                        //sp.transform.position = pos[i];
                        sp.GetComponent<Rigidbody>().AddForce(muzzle[i].transform.forward * 500f);
                        //splash.velocity = transform.TransformDirection(Vector3.forward * 1000f);
                        //splashBullet[i] = Instantiate(splash, pos[i], transform.rotation) as Rigidbody;
                        //splashBullet[i].velocity = transform.TransformDirection(Vector3.forward * 1f);

                    }
                    notSplash = true;
                }
                //if(splashCount >0)
                //{
                //    splashCount -= 1;
                //    notSplash = true;
                //    for (int i = 0; i < 1; i++)
                //    {
                //        splashBullet = Instantiate(splash, pos[i], transform.rotation) as Rigidbody;
                //        splashBullet.velocity = transform.TransformDirection(Vector3.forward * 1f);
                //    }

                //}
                Debug.Log("�����т���т���U��");
            }

            
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
