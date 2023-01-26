using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIMove2 : MonoBehaviour
{
    //-------------------------------------------------------------------
    //  �X�e�[�g�̍쐬
    //-------------------------------------------------------------------
    public enum BossState
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    }
    class Desire
    {
        public BossState state{ get; private set; /* ���O�����珑��������s�ɂ���*/}
        public float value;

        public Desire(BossState _state)
        {
            state = _state;
            value = 0f;
        }
    }
    class Desires
    {
        // ---------���X�g�쐬
        public List<Desire> desireList { get; private set; } = new List<Desire>();
        // ---------���\�b�h�쐬
        public Desire GetDesire(BossState state)
        {
            foreach (Desire desire in desireList)        //desireList�̒��g������`�F�b�N
            {
                if (desire.state == state)     //�����ƃ`�F�b�N���Ă���Desire�̃^�C�v�������Ȃ�
                {
                    return desire;
                }
            }
            return null;
        }
        // ---------���X�g�̗D�揇�ʍ쐬
        public void SortDesire()
        {
            //                  ��desire2��value�̒l��desire1��value�̒l���傫�����desire1��desire2�����ւ���
            desireList.Sort((desire1, desire2) => desire2.value.CompareTo(desire1.value));
        }
        public Desires()
        {
            //                           ��GetNames...�S�Ă̗v�f�̖��O�𕶎���̔z��ɕϊ�
            int desireNum = System.Enum.GetNames(typeof(BossState)).Length;
            for (int i = 0; i < desireNum; i++)         //�U���p�^�[�����J��Ԃ�
            {
                BossState state = (BossState)System.Enum.ToObject(typeof(BossState), i);
                Desire newDesire = new Desire(state);

                desireList.Add(newDesire);
            }
        }
    }

    Desires desires = new Desires();

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
    //�@�v���C���[Transform
    private Transform playerTransform;

    private float moveTime;
    [SerializeField] private float limitmoveTime = 5f;

    [SerializeField] float freezeTime = 0.5f;

    //-------------------------------------------------------------------
    //  Boss�p�^�[�����e�������
    //-------------------------------------------------------------------
    float _walkPeriod = 5.0f;
    float _walkTime = 1.0f;

    float _waitPeriod = 5.0f;
    float _waitTime = 1.0f;

    //-------------------------------------------------------------------
    //  �X�e�[�g�ϊ��p
    //-------------------------------------------------------------------
    //void ChangeState(State newState)         //state�ϊ��p
    //{
    //    currentState = newState;             //�X�e�[�g�̐؂�ւ�
    //    stateEnter = true;                   //�X�e�[�g�̐؂�ւ����}
    //}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
