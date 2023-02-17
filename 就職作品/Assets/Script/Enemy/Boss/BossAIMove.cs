using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIMove : MonoBehaviour
{
    //--------------------�X�e�[�g�쓮
    public enum BossState
    {
        Walk,                   //����p�^�[��
        Wait,                   //�����~�p�^�[��
        Chase,                  //�ǐՃp�^�[��
        Attack,                 //�ʏ�U���p�^�[��
        EruptionAttack,         //���΍U���p�^�[��
        BackJump,               //������荞�݃p�^�[��
        Splash,                 //�͈͍U���p�^�[��
        Freeze                  //��
    };
    //�|�W�V�����̎擾
    public Vector3 bossPos = new Vector3();
    //�L�����N�^�[�ړ�
    private CharacterController _bossController;
    //�A�j���[�V����
    private Animator animator;
    //�@�ړI�n
    private Vector3 destination;
    //�@�����X�s�[�h
    [SerializeField] private float walkSpeed = 1.0f;
    //�@���x
    private Vector3 velocity;
    //�@�ړ�����
    private Vector3 direction;
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

    //���΍U���p�^�[���@�錾
    private eruptionAttack eruptionAttackFlag;              //�U���t���O
    private float eruptionCountTime;                        //�A�ˉ�
    public int eruptionBulletCount = 1;                     //�ł��~�߉�
    private float eruptionBulletInterval;                   //�U����̒�~����
    private float eruptionReloadInterval;                   //�U�����ߎ���
    public bool notEruptionBullt;                           //�e�؂�t���O
    bool haveEruptionBullet;                                //�s���s�\�t���O
    public GameObject posEruptionBullet;                    //�e�I�u�W�F�N�g�����ʒu
    public Rigidbody eruptionBullet;                        //�e�I�u�W�F�N�g�擾


    [SerializeField] private float beforeEruptionWait = 1f; //�U�����ߎ���

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
            else if (eruptionAttackFlag == true)    //���΍U���p�^�[��
            {
                //Debug.Log("���΍U��");
                SetState(BossState.EruptionAttack); //�X�e�[�g�ړ�
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
        else if(tempState == BossState.EruptionAttack)  //���΍U���p�^�[��
        {
            velocity = Vector3.zero;                    //�ړ��ʏ�����
            Rigidbody clone;                            //Rigidbody�擾
            if (eruptionBulletCount == 0)   //�U���p�̒e���Ȃ��Ȃ�
            {
                haveEruptionBullet = true;  //�s���s�\
            }
            eruptionReloadInterval += Time.deltaTime;   //���ߎ��ԊJ�n
            if(eruptionReloadInterval > 3)              //���ߎ��Ԃ�3�b��������
            {
                eruptionBulletInterval += Time.deltaTime;   //�U����̒�~���ԊJ�n
                if(eruptionBulletCount > 0) //�U���p�̒e������Ȃ�
                {
                    eruptionBulletCount -= 1;   //�e�����炷
                    //���ˈʒu�𐶐�
                    GameObject sp = Instantiate(splashbuullet, posEruptionBullet.transform.position, transform.rotation);
                    Destroy(sp, 3.0f);          //3�b��ɏ���
                    //�e�𐶐�
                    clone = Instantiate(eruptionBullet, posEruptionBullet.transform.position, transform.rotation) as Rigidbody;
                    //���˕����ɗ͂�������
                    clone.velocity = transform.TransformDirection(Vector3.up * 3f);
                    notEruptionBullt = true;    //�e�؂�łȂ�
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
