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
        //　見回りまたはキャラクターを追いかける状態
        if (state == BossState.Walk || state == BossState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == BossState.Chase)
            {
                //Debug.Log("追跡");
                //Debug.Log(eruptionAttackFlag);
                setPosition.SetDestination(playerTransform.position);
            }
            #region
            //if (state == BossState.Walk)
            //{
            //    Debug.Log("巡回中");
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
            //    Debug.Log("行動制限時間中");
            //    moveTime += Time.deltaTime;
            //    Debug.Log(moveTime);
            //}
            //if (state == BossState.Wait)
            //{
            //    Debug.Log("行動制限時間");
            //    elapsedTime += Time.deltaTime;
            //    Debug.Log(elapsedTime);
            //    if (elapsedTime >= waitTime)
            //    {
            //        Debug.Log("巡回を再開");
            //        SetState(BossState.Walk);
            //    }
            //}
            #endregion
            //　キャラクターが地面にいる状態
            if (_bossController.isGrounded)
            {
                velocity = Vector3.zero;
                velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }
            //　巡回状態
            if (state == BossState.Walk)
            {
                //Debug.Log("巡回");
                walkTime += Time.deltaTime;
                //5秒以上巡回したらステートを変更
                if (walkTime > 5)
                {
                    //Debug.Log("巡回制限時間");
                    SetState(BossState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
                //　目的地に到着したかどうかの判定
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 2f)
                {
                    //Debug.Log("到着");
                    SetState(BossState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
            }
            //　追跡状態
            else if (state == BossState.Chase)
            {
                //　攻撃する距離だったら攻撃
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1f)
                {
                    //Debug.Log("攻撃");
                    SetState(BossState.Attack);
                }
            }
            else if (eruptionAttackFlag == true) 
            {
                //Debug.Log("噴火攻撃");
                SetState(BossState.EruptionAttack);
            }
            else if (backJumpFlag == true)
            {
                //Debug.Log("後ろジャンプ");
                SetState(BossState.BackJump);
            }
            else if (splashFlag == true)
            {
                SetState(BossState.Splash);
            }
        }
        // 到着していたら一定時間待つ
        else if (state == BossState.Wait)
        {
            //Debug.Log("停止");
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                //Debug.Log("巡回を再開");
                SetState(BossState.Walk);
            }
        }
        else if (state == BossState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > freezeTime)
            {
                //Debug.Log("巡回を再開する");
                SetState(BossState.Walk);
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        _bossController.Move(velocity * Time.deltaTime);
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
            //animator.SetFloat("Speed", 1f);
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
            //Debug.Log("噴火攻撃");
        }
        else if(tempState == BossState.BackJump)
        {
            //後ろに移動とジャンプを同時に行う
            if (haveJump == true)
            {
                //jumpForceの分だけ上方に力がかかる
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

            //Debug.Log("後ろジャンプ");
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
                Debug.Log("噴水びちゃびちゃ攻撃");
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
    
    //　敵キャラクターの状態取得メソッド
    public BossState GetState()
    {
        return state;
    }
}
