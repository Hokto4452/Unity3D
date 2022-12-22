using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    //--------------- 変数宣言 --------------------
    [SerializeField] private Transform character;
    [SerializeField] private Transform pivot;

    public float _rotationSpeed = 1f;        //回転速度
    public float _max_rotation_x = 60f;      //x軸回転角度の最大値
    private float _rotation_x = 0f;          //現在のx軸回転角度
    private float _rotation_y = 0f;          //現在のy軸回転角度

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            character = transform.parent;
        }
        if (pivot == null)
        {
            pivot = transform;
        }

    }

    //カメラ上下移動の最大、最小角度です。Inspectorウィンドウから設定してください
    [Range(-0.999f, -0.5f)]
    public float maxYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    public float minYAngle = 0.5f;

    // Update is called once per frame
    void Update()
    {
        moveMouse3rdPlayer();
        //moveMouse1stPlayer();
        //shotBullet();
    }

    void shotBullet()
    {
        if (Input.GetMouseButton(0))     //左クリック
        {
            //↓一定間隔(速め)で弾を撃ちたい

            //Debug.Log("左ボタンが押されています。");
        }
        if (Input.GetMouseButton(1))     //右クリック
        {
            //↓エイム
            //Debug.Log("左ボタンが押されています。");
        }
        //if (Input.GetMouseButton(2))     //ホイール
        //{
        //    //Debug.Log("マウスホイールが離されました。");
        //}
    }

 
    void moveMouse3rdPlayer()
    {
        float X_Rotation = Input.GetAxis("Mouse X");        //X軸移動量
        float Y_Rotation = Input.GetAxis("Mouse Y");        //Y軸移動量

        character.transform.Rotate(0, X_Rotation*3, 0);       //プレイヤーの回転
        
        float nowAngle = pivot.transform.localRotation.x;   //現在の角度
        pivot.transform.Rotate(-Y_Rotation, 0, 0);  //下向きにカメラ方向移動
        pivot.transform.Rotate(-Y_Rotation, 0, 0);  //上向きにカメラ方向移動

    }

    void moveMouse1stPlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //回転角度を変更
            _rotation_y -= _rotationSpeed;
            //y軸を軸に左回りにrotationSpeed度回転
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //回転角度を変更
            _rotation_y += _rotationSpeed;
            //y軸を軸に左回りにrotationSpeed度回転
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //カメラの縦方向の角度の範囲を指定
            if (_rotation_x < -_max_rotation_x)
            {
                //範囲外のときreturn
                return;
            }
            //回転角度を変更
            _rotation_x -= _rotationSpeed;
            //x軸を軸に上方向に回転
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //カメラの縦方向の角度の範囲を指定
            if (_rotation_x > _max_rotation_x)
            {
                //範囲外のときreturn
                return;
            }
            //回転角度を変更
            _rotation_x += _rotationSpeed;
            //x軸を軸に上方向に回転
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
    }
    
}
