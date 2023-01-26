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
    private EnemyState state;
    //　プレイヤーTransform
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

        //        //　目的地に到着したかどうかの判定
        //        if (Vector3.Distance(transform.position, destination) < 0.5f)
        //        {
        //            arrived = true;
        //            animator.SetFloat("Speed", 0.0f);
        //        }
        //        else
        //        {
        //            elapsedTime += Time.deltaTime;

        //            //　待ち時間を越えたら次の目的地を設定
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
        //　見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == EnemyState.Chase)
            {
                Debug.Log("追跡中");
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
                Debug.Log("巡回");
                walkTime += Time.deltaTime;
                if (walkTime > 5)
                {
                    Debug.Log("巡回制限時間");
                    SetState(EnemyState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
                //　目的地に到着したかどうかの判定
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 2f)
                {
                    Debug.Log("到着");
                    SetState(EnemyState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
                
            }
            else if (state == EnemyState.Chase)
            {
                //　攻撃する距離だったら攻撃
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1f)
                {
                    Debug.Log("攻撃をします");
                    SetState(EnemyState.Attack);
                }
            }
            // 到着していたら一定時間待つ
        }
        else if (state == EnemyState.Wait)
        {
            Debug.Log("停止します");
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            //　待ち時間を越えたら次の目的地を設定
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
    //　敵キャラクターの状態変更メソッド
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
            //　待機状態から追いかける場合もあるのでOff
            arrived = false;
            //　追いかける対象をセット
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

    //    //　HP表示用UIのアップデート
    //    UpdateHPValue();

    //    if (hp <= 0)
    //    {
    //        //　HP表示用UIを非表示にする
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

    ////　死んだらHPUIを非表示にする
    //public void HideStatusUI()
    //{
    //    HPUI.SetActive(false);
    //}

    //public void UpdateHPValue()
    //{
    //    hpSlider.value = (float)GetHp() / (float)GetMaxHp();
    //}

    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState()
    {
        return state;
    }
}
