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
    // 敵の状態
    private BossState state;
    //　プレイヤーTransform
    private Transform playerTransform;

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
    }

    // Update is called once per frame
    void Update()
    {
        //　見回りまたはキャラクターを追いかける状態
        if (state == BossState.Walk || state == BossState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == BossState.Chase)
            {
                setPosition.SetDestination(playerTransform.position);
            }
            //　キャラクターが地面にいる状態
            if (_bossController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }

            //　目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 2f)
            {
                SetState(BossState.Wait);
                animator.SetFloat("Speed", 0.0f);
            }
            else if (state == BossState.Chase)
            {
                //　攻撃する距離だったら攻撃
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1f)
                {
                    SetState(BossState.Attack);
                }
            }
            // 到着していたら一定時間待つ
            else if (state == BossState.Wait)
            {
                elapsedTime += Time.deltaTime;

                //　待ち時間を越えたら次の目的地を設定
                if (elapsedTime > waitTime)
                {
                    SetState(BossState.Walk);
                }
            }
            else if (state == BossState.Freeze)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime > freezeTime)
                {
                    SetState(BossState.Walk);
                }
            }
            velocity.y += Physics.gravity.y * Time.deltaTime;
            _bossController.Move(velocity * Time.deltaTime);
        }
    }
    //　敵キャラクターの状態変更メソッド
    public void SetState(BossState tempState, Transform targetObj = null)
    {
        if (tempState == BossState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            state = tempState;
            setPosition.CreateRandomPosition();
        }
        else if (tempState == BossState.Chase)
        {
            state = tempState;
            //　待機状態から追いかける場合もあるのでOff
            arrived = false;
            //　追いかける対象をセット
            playerTransform = targetObj;
        }
        else if (tempState == BossState.Wait)
        {
            elapsedTime = 0f;
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

    //　敵キャラクターの状態取得メソッド
    public BossState GetState()
    {
        return state;
    }
}
