using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //--------------- 変数宣言 --------------------
    private Rigidbody _rigidbody;   //物理判定
    private Transform _transform;   //移動処理
    private Animator _animator;     //アニメーション
    private float _horizontal;      //横軸
    private float _vertical;        //縦軸
    private Vector3 _velocity;      //ベクトル量

    private float _speed = 3f;          //スピード
    private float _topSpeed = 6f;       //ダッシュスピード

    private bool _runFlag;

    private Vector3 _aim;               //方向
    private Quaternion _playerRotation; //回転

    float jumpForce = 600.0f;           //ジャンプ力
    Vector3 playerPos;                  //ユニティちゃんの位置を入れる
    bool Ground = true;                 //地面に接触しているか否か
    int key = 0;

    public InputField inputField;
    public GameObject door;
    public GameObject fieldObject;


    public float sight_x = 0;
    public float sight_y = 0;
    private bool isFront;
    private bool isBack;
    private bool isLeft;
    private bool isRight;
    public float moveSpd;

    //float x = Input.GetAxis("Horizontal");
    //float z = Input.GetAxis("Vertical");
    //float angleH = Input.GetAxis("Horizontal");
    //float angleV = Input.GetAxis("Vertical");

    //--------------- 初期 --------------------
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();     //物理処理取得
        _transform = GetComponent<Transform>();     //移動処理取得
        _animator = GetComponent<Animator>();       //アニメーション取得

        _playerRotation = _transform.rotation;      //回転処理

        _runFlag = false;                           //ダッシュ用フラグ
        playerPos = transform.position;
        }

    //--------------- 更新 --------------------
    void FixedUpdate()
    {
        //DebugLog();
        //movePlayer();
        jumpPlayer();
        ProCon3rd();
        ProConMove();
       
    }

    void DebugLog()
    {
        #region
        //L Stick
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        //R Stick
        float rsh = Input.GetAxis("R_Stick_H");
        float rsv = Input.GetAxis("R_Stick_V");
        //D-Pad
        float dph = Input.GetAxis("D_Pad_H");
        float dpv = Input.GetAxis("D_Pad_V");
        //Trigger
        float tri = Input.GetAxis("L_R_Trigger");

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("joystick button 0"))
        {
            Debug.Log("A");
        }   //A
        if (Input.GetKeyDown("joystick button 1"))
        {
            Debug.Log("B");
        }   //B
        if (Input.GetKeyDown("joystick button 2"))
        {
            Debug.Log("Y");
        }   //Y
        if (Input.GetKeyDown("joystick button 3"))
        {
            Debug.Log("X");
        }   //X
        if (Input.GetKeyDown("joystick button 4"))
        {
            Debug.Log("L");
        }   //L
        if (Input.GetKeyDown("joystick button 5"))
        {
            Debug.Log("R");
        }   //R
        if (Input.GetKeyDown("joystick button 6"))
        {
            Debug.Log("ZL");
        }   //ZL
        if (Input.GetKeyDown("joystick button 7"))
        {
            Debug.Log("ZR");
        }   //ZR
        if (Input.GetKeyDown("joystick button 8"))
        {
            Debug.Log("-");
        }   //-
        if (Input.GetKeyDown("joystick button 9"))
        {
            Debug.Log("+");
        }   //+
        if ((hori != 0) || (vert != 0))           
        {
            // Debug.Log("stick:" + hori + "," + vert);
        }              //Stick
        if ((lsh != 0) || (lsv != 0))
        {
           //Debug.Log("L stick:" + lsh + "," + lsv);   
        }                //L Stick
        if ((rsh != 0) || (rsv != 0))
        {
            //Debug.Log("R Stick:" + rsh + "," + rsv);
        }                //R Stick
        if ((dph != 0) || (dpv != 0)) 
        {
            //Debug.Log("HV 十字キー:" + dph + "," + dpv);
        }                //HV 十字キー
        if (tri > 0)
        {
            //Debug.Log("dddd:" + tri);
        }
        else if (tri < 0)
        {
            //Debug.Log("eee:" + tri);
        }
        else
        {
            //.Log("  trigger:none");
        }
        #endregion

    }

    public void ProCon3rd()
    {
        float angleH = Input.GetAxis("R_Stick_H");
        float angleV = Input.GetAxis("R_Stick_V");

        if (sight_y > 80)
        {
            if (angleV < 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else if (sight_y < -90)
        {
            if (angleV > 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else
        {
            sight_y = sight_y + angleV;
        }

        if (sight_x >= 360)    //sight_x が360度を超えると360を引く、超えた分の端数はsight_xに残る
        {
            sight_x = sight_x - 360;
        }
        else if (sight_x < 0)  //sight_x が0度を下回ると360からsight_xを引く、残った分はsight_xに残る
        {
            sight_x = 360 - sight_x;
        }
        sight_x = sight_x + angleH;
        transform.localRotation = Quaternion.Euler(sight_y, sight_x, 0);
    }

    void ProConMove()
    {
        float moveH = Input.GetAxis("L_Stick_H");
        float moveV = Input.GetAxis("L_Stick_V");

        _animator.SetBool("walking", false);    //アニメーション実行しない
        _animator.SetBool("running", false);    //アニメーション実行しない

        //--------------------------------------
        //'''''キャラクター移動'''''
        //--------------------------------------
        isFront = moveV > 0;
        isBack = moveV < 0;
        isLeft = moveH < 0;
        isRight = moveH > 0;
        
        //--------------------------------------
        //'''''スピード制限'''''
        //--------------------------------------
        moveSpd = 3f * (Mathf.Abs(moveV) + Mathf.Abs(moveH));

        if (moveSpd > 4f)          //スピード制限
        {
            moveSpd = 3.9f;
        }
        else if (moveSpd <= 4f)
        {
            moveSpd = 3f * (Mathf.Abs(moveV) + Mathf.Abs(moveH));
        }
        //Vector3 v = new Vector3(0f, 0f, 0f);

        if (isFront)
        {

            transform.position += transform.forward * moveSpd * Time.deltaTime;
            if (moveSpd >= 3)
            {
                runFlag();
            }
            _animator.SetBool("walking", true);     //アニメーション実行

        }
        if (isBack)
        {
            transform.position -= transform.forward * moveSpd * Time.deltaTime;
            _animator.SetBool("walking", true);     //アニメーション実行
        }
        if (isLeft)
        {
            transform.position -= transform.right * moveSpd * Time.deltaTime;
            _animator.SetBool("walking", true);     //アニメーション実行
        }
        if (isRight)
        {
            transform.position += transform.right * moveSpd * Time.deltaTime;
            _animator.SetBool("walking", true);     //アニメーション実行
        }
        
    }

    //---------移動関数
    void movePlayer()
    {
        _animator.SetBool("walking", false);    //アニメーション実行しない
        _animator.SetBool("running", false);

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                runFlag();
            }
            else
            {
                _speed = 3;
            }
            transform.position += transform.forward * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //アニメーション実行
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //アニメーション実行
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //アニメーション実行
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * _speed * Time.deltaTime;
            _animator.SetBool("walking", true);     //アニメーション実行
        }

        #region
        //    _horizontal = Input.GetAxis("Horizontal");  //横移動ボタン判定
        //    _vertical = Input.GetAxis("Vertical");      //縦移動ボタン判定

        //    var _horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);            //横回転

        //    _velocity = _horizontalRotation * new Vector3(_horizontal, _rigidbody.velocity.y, _vertical).normalized;    //横回転移動量

        //    _aim = _horizontalRotation * new Vector3(_horizontal, 0, _vertical).normalized;                             //方向決定
        //    if (_aim.magnitude > 0.5f)
        //    {
        //        _playerRotation = Quaternion.LookRotation(_aim, Vector3.up);    //回転実行
        //    }

        //    _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _playerRotation, 600 * Time.deltaTime); //移動実行

        //    if (_velocity.magnitude > 0.1f)
        //    {
        //        _animator.SetBool("walking", true);     //アニメーション実行
        //    }
        //    else
        //    {
        //        _animator.SetBool("walking", false);    //アニメーション不実行
        //    }

        //    _rigidbody.velocity = _velocity * _speed;   //移動量決定
        #endregion

    }

    void runFlag()
    {
        _runFlag = true;
        _speed = _topSpeed;
        _animator.SetBool("running", true);
    }

    //---------ジャンプ関数
    void jumpPlayer()
    {
        if (Ground)
        {
            if (Input.GetButton("Jump"))
            {
                //jumpForceの分だけ上方に力がかかる
                _rigidbody.AddForce(transform.up * jumpForce);
                Ground = false;
            }
        }
    }

    //ジャンプ後、Planeに接触した時に接触判定をtrueに戻す
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!Ground)
                Ground = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Door_snaps")
            fieldObject.SetActive(true);
    }

    public void InputPass()
    {
        if (inputField.text == "4452")
            door.GetComponent<ClearDoorOpen>().DoorMove();
        fieldObject.SetActive(false);

    }


}

