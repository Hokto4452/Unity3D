using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIMove2 : MonoBehaviour
{
    //-------------------------------------------------------------------
    //  ステートの作成
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
        public BossState state{ get; private set; /* ←外部から書き換えを不可にする*/}
        public float value;

        public Desire(BossState _state)
        {
            state = _state;
            value = 0f;
        }
    }
    class Desires
    {
        // ---------リスト作成
        public List<Desire> desireList { get; private set; } = new List<Desire>();
        // ---------メソッド作成
        public Desire GetDesire(BossState state)
        {
            foreach (Desire desire in desireList)        //desireListの中身を一個ずつチェック
            {
                if (desire.state == state)     //引数とチェックしているDesireのタイプが同じなら
                {
                    return desire;
                }
            }
            return null;
        }
        // ---------リストの優先順位作成
        public void SortDesire()
        {
            //                  ↓desire2のvalueの値がdesire1のvalueの値より大きければdesire1とdesire2を入れ替える
            desireList.Sort((desire1, desire2) => desire2.value.CompareTo(desire1.value));
        }
        public Desires()
        {
            //                           ↓GetNames...全ての要素の名前を文字列の配列に変換
            int desireNum = System.Enum.GetNames(typeof(BossState)).Length;
            for (int i = 0; i < desireNum; i++)         //攻撃パターン分繰り返す
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
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField] private float walkSpeed = 1.0f;//
    //　速度
    private Vector3 velocity;                       //
    //　移動方向
    private Vector3 direction;                      //
    //　到着フラグ
    private bool arrived;
    //  スクリプト
    private MovePosition setPosition;
    //　待ち時間
    [SerializeField] private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;
    //　プレイヤーTransform
    private Transform playerTransform;

    private float moveTime;
    [SerializeField] private float limitmoveTime = 5f;

    [SerializeField] float freezeTime = 0.5f;

    //-------------------------------------------------------------------
    //  Bossパターン内容初期情報
    //-------------------------------------------------------------------
    float _walkPeriod = 5.0f;
    float _walkTime = 1.0f;

    float _waitPeriod = 5.0f;
    float _waitTime = 1.0f;

    //-------------------------------------------------------------------
    //  ステート変換用
    //-------------------------------------------------------------------
    //void ChangeState(State newState)         //state変換用
    //{
    //    currentState = newState;             //ステートの切り替え
    //    stateEnter = true;                   //ステートの切り替え合図
    //}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
