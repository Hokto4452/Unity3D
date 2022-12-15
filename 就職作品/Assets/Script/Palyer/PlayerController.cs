using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Vector3 moveDirection = Vector3.zero;
    Vector3 startPos;


    //--------------- 初期 --------------------
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();     //物理処理取得
        _transform = GetComponent<Transform>();     //移動処理取得
        _animator = GetComponent<Animator>();       //アニメーション取得

        _playerRotation = _transform.rotation;      //回転処理

        _runFlag = false;                           //ダッシュ用フラグ
    }

    //--------------- 更新 --------------------
    void FixedUpdate()
    {
        movePlayer();
        //jumpPlayer();
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
       
    }

    //---------マウス移動
    
}

