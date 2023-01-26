using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovrAI3 : MonoBehaviour
{
    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    };

    private CharacterController enemyController;    //
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
    private EnemyState state;
    //�@�v���C���[Transform
    private Transform playerTransform;

    private float walkTime;

    [SerializeField] float freezeTime = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        setPosition = GetComponent<MovePosition>();
        setPosition.CreateRandomPosition();
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(EnemyState.Walk);
        walkTime = 0f;

        //enemy = GetComponent<Enemy>();
        //hp = maxHp;
        //hpSlider = HPUI.transform.Find("HPBar").GetComponent<Slider>();
        //hpSlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        #region
        //    if (!arrived)
        //    {
        //        if (enemyController.isGrounded)
        //        {
        //            velocity = Vector3.zero;
        //            //animator.SetFloat("Speed", 2.0f);
        //            direction = (destination - transform.position).normalized;
        //            transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
        //            velocity = direction * walkSpeed;
        //            //Debug.Log (destination);
        //        }
        //        velocity.y += Physics.gravity.y * Time.deltaTime;
        //        enemyController.Move(velocity * Time.deltaTime);

        //        //�@�ړI�n�ɓ����������ǂ����̔���
        //        if (Vector3.Distance(transform.position, destination) < 0.5f)
        //        {
        //            arrived = true;
        //            animator.SetFloat("Speed", 0.0f);
        //        }
        //        else
        //        {
        //            elapsedTime += Time.deltaTime;

        //            //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
        //            if (elapsedTime > waitTime)
        //            {
        //                setPosition.CreateRandomPosition();
        //                destination = setPosition.GetDestination();
        //                arrived = false;
        //                elapsedTime = 0f;
        //            }
        //        }
        //    }
        #endregion
        //�@�����܂��̓L�����N�^�[��ǂ���������
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //�@�L�����N�^�[��ǂ��������Ԃł���΃L�����N�^�[�̖ړI�n���Đݒ�
            if (state == EnemyState.Chase)
            {
                Debug.Log("�ǐՒ�");
                setPosition.SetDestination(playerTransform.position);
            }
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }
            if (state == EnemyState.Walk)
            {
                Debug.Log("����");
                walkTime += Time.deltaTime;
                if (walkTime > 5)
                {
                    Debug.Log("���񐧌�����");
                    SetState(EnemyState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
                //�@�ړI�n�ɓ����������ǂ����̔���
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 2f)
                {
                    Debug.Log("����");
                    SetState(EnemyState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
                
            }
            else if (state == EnemyState.Chase)
            {
                //�@�U�����鋗����������U��
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1f)
                {
                    Debug.Log("�U�������܂�");
                    SetState(EnemyState.Attack);
                }
            }
            // �������Ă������莞�ԑ҂�
        }
        else if (state == EnemyState.Wait)
        {
            Debug.Log("��~���܂�");
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
            if (elapsedTime > waitTime)
            {
                SetState(EnemyState.Walk);
            }
        }
        else if (state == EnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > freezeTime)
            {
                SetState(EnemyState.Walk);
            }
        }
            velocity.y += Physics.gravity.y * Time.deltaTime;
            enemyController.Move(velocity * Time.deltaTime);
    }
    //�@�G�L�����N�^�[�̏�ԕύX���\�b�h
    public void SetState(EnemyState tempState, Transform targetObj = null)
    {
        if (tempState == EnemyState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            state = tempState;
            setPosition.CreateRandomPosition();
        }
        else if (tempState == EnemyState.Chase)
        {
            state = tempState;
            //�@�ҋ@��Ԃ���ǂ�������ꍇ������̂�Off
            arrived = false;
            //�@�ǂ�������Ώۂ��Z�b�g
            playerTransform = targetObj;
        }
        else if (tempState == EnemyState.Wait)
        {
            elapsedTime = 0f;
            state = tempState;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
        else if (tempState == EnemyState.Attack)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", true);
        }
        else if (tempState == EnemyState.Freeze)
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", false);
        }
    }

    //public void SetHp(int hp)
    //{
    //    this.hp = hp;

    //    //�@HP�\���pUI�̃A�b�v�f�[�g
    //    UpdateHPValue();

    //    if (hp <= 0)
    //    {
    //        //�@HP�\���pUI���\���ɂ���
    //        HideStatusUI();
    //    }
    //}

    //public int GetHp()
    //{
    //    return hp;
    //}

    //public int GetMaxHp()
    //{
    //    return maxHp;
    //}

    ////�@���񂾂�HPUI���\���ɂ���
    //public void HideStatusUI()
    //{
    //    HPUI.SetActive(false);
    //}

    //public void UpdateHPValue()
    //{
    //    hpSlider.value = (float)GetHp() / (float)GetMaxHp();
    //}

    //�@�G�L�����N�^�[�̏�Ԏ擾���\�b�h
    public EnemyState GetState()
    {
        return state;
    }
}
