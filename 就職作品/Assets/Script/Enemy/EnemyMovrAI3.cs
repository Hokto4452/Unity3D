using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyMovrAI3 : MonoBehaviour
{
    public enum EnemyState
    {
        Walk,
        Wait,
        Chase
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

    

    // Start is called before the first frame update
    void Start()
    {
        setPosition = GetComponent<MovePosition>();
        setPosition.CreateRandomPosition();
        enemyController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(EnemyState.Walk);
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
                setPosition.SetDestination(playerTransform.position);
            }
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                //animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }

            //　目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 2f)
            {
                SetState(EnemyState.Wait);
                //animator.SetFloat("Speed", 0.0f);
            }
            //　到着していたら一定時間待つ
        }
        else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;

            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
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
    }
    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState()
    {
        return state;
    }
}
