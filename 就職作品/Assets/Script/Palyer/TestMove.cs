using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    //Rigidbodyを変数に入れる
    Rigidbody rb;
    //移動スピード
    float speed = 3.0f;
    //ジャンプ力
    float jumpForce = 2000.0f;
    //ユニティちゃんの位置を入れる
    Vector3 playerPos;
    //地面に接触しているか否か
    bool Ground = true;
    int key = 0;



    private bool isFront;
    private bool isBack;
    private bool isLeft;
    private bool isRight;
    private bool isUp;
    private bool isDown;
    private Vector3 AroundPos;
    public float moveSpd;


    public void Hit()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //ユニティちゃんの現在より少し前の位置を保存
        playerPos = transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        //GetInputKey();
        SightProcon();
        //Move();
        //ProConButton();
    }



    void ProConButton()
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

        

        if (Input.GetKeyDown("joystick button 0"))      //A
        {
            Debug.Log("button0");
        }
        if (Input.GetKeyDown("joystick button 1"))      //A
        {
            Debug.Log("button1");
        }
        if (Input.GetKeyDown("joystick button 2"))      //A
        {
            Debug.Log("button2");
        }
        if (Input.GetKeyDown("joystick button 3"))      //A
        {
            Debug.Log("button3");
        }
        if (Input.GetKeyDown("joystick button 4"))      //A
        {
            Debug.Log("button4");
        }
        if (Input.GetKeyDown("joystick button 5"))      //A
        {
            Debug.Log("button5");
        }
        if (Input.GetKeyDown("joystick button 6"))      //A
        {
            Debug.Log("button6");
        }
        if (Input.GetKeyDown("joystick button 7"))      //A
        {
            Debug.Log("button7");
        }
        if (Input.GetKeyDown("joystick button 8"))      //A
        {
            Debug.Log("button8");
        }
        if (Input.GetKeyDown("joystick button 9"))      //A
        {
            Debug.Log("button9");
        }


        
        if ((hori != 0) || (vert != 0))                 //A
        {
            //Debug.Log("stick:" + hori + "," + vert);
        }

        if ((lsh != 0) || (lsv != 0))               //L Stick
        {
            Debug.Log("L stick:" + lsh + "," + lsv);
        }
       
        if ((rsh != 0) || (rsv != 0))               //R Stick
        {
            Debug.Log("R stick:" + rsh + "," + rsv);
        }
        
        if ((dph != 0) || (dpv != 0))               //D-Pad
        {
            Debug.Log("D Pad:" + dph + "," + dpv);
        }
        
        if (tri > 0)                                //Trigger
        {
            Debug.Log("L trigger:" + tri);
        }
        else if (tri < 0)
        {
            Debug.Log("R trigger:" + tri);
        }
        else
        {
            //.Log("  trigger:none");
        }
        #endregion

        //--------------------------------------
        //'''''キャラクター移動'''''
        //--------------------------------------
        isFront = vert > 0;
        isBack = vert < 0;
        isLeft = hori < 0;
        isRight = hori > 0;

        //--------------------------------------
        //'''''スピード制限'''''
        //--------------------------------------
        moveSpd = 2f * (Mathf.Abs(vert) + Mathf.Abs(hori));

        if (moveSpd > 1.5f)          //スピード制限
        {
            moveSpd = 1.5f;
        }
        else if (moveSpd <= 1.5f)
        {
            moveSpd = 2f * (Mathf.Abs(vert) + Mathf.Abs(hori));
        }


        Vector3 v = new Vector3(0f, 0f, 0f);

        if (isFront) v.z = moveSpd;
        if (isBack) v.z = -moveSpd;
        if (isLeft) v.x = -moveSpd;
        if (isRight) v.x = moveSpd;
        rb.velocity = v;

        //animator.SetFloat("MoveSpeed", new Vector3(v.x, 0, v.z).magnitude);
        //Vector3 diff = transform.position - AroundPos;
        //AroundPos = transform.position;

        //if (diff.magnitude > 0.01f)
        //{
        //    transform.rotation = Quaternion.LookRotation(diff);
        //}

    }



    void GetInputKey()
    {
        //A・Dキー、←→キーで横移動
        //float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpd;

        //W・Sキー、↑↓キーで前後移動
        //float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpd;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }
    }

    float hori = Input.GetAxis("Horizontal");
    float vert = Input.GetAxis("Vertical");
    
    float sight_x = 0;
    float sight_y = 0;

    void SightProcon()
    {
        float angleH = Input.GetAxis("Horizontal2");
        float angleV = Input.GetAxis("Vertical2");

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
    void Move()
    {
        isFront = vert > 0;
        isBack = vert < 0;
        isLeft = hori < 0;
        isRight = hori > 0;
        moveSpd = 2f * (Mathf.Abs(vert) + Mathf.Abs(hori));
        moveSpd = 2f * (Mathf.Abs(vert) + Mathf.Abs(hori));

        if (moveSpd > 1.5f)          //スピード制限
        {
            moveSpd = 1.5f;
        }
        else if (moveSpd <= 1.5f)
        {
            moveSpd = 2f * (Mathf.Abs(vert) + Mathf.Abs(hori));
        }
        Vector3 v = new Vector3(0f, 0f, 0f);

        if (isFront) v.z = moveSpd;
        if (isBack) v.z = -moveSpd;
        if (isLeft) v.x = -moveSpd;
        if (isRight) v.x = moveSpd;
        rb.velocity = v;



        if (Ground)
        {
            if (Input.GetButton("Jump"))
            {
                //jumpForceの分だけ上方に力がかかる
                rb.AddForce(transform.up * jumpForce);
                Ground = false;
            }

        }

        //現在の位置＋入力した数値の場所に移動する
        //rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));
        rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpd, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpd));

        //ユニティちゃんの最新の位置から少し前の位置を引いて方向を割り出す
        Vector3 direction = transform.position - playerPos;

        //移動距離が少しでもあった場合に方向転換
        if (direction.magnitude >= 0.01f)
        {
            //directionのX軸とZ軸の方向を向かせる
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        else
        {
            key = 0;
        }

        //ユニティちゃんの位置を更新する
        playerPos = transform.position;

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






    ////Rigidbodyを変数に入れる
    //Rigidbody rb;
    ////移動スピード
    //float speed = 3.0f;
    ////ジャンプ力
    //float jumpForce = 400.0f;
    ////Animatorを入れる変数
    //private Animator animator;
    ////ユニティちゃんの位置を入れる
    //Vector3 playerPos;
    ////地面に接触しているか否か
    //bool Ground = true;
    //int key = 0;

    //string state;
    //string prevState;


    //void Start()
    //{
    //    //Rigidbodyを取得
    //    rb = GetComponent<Rigidbody>();
    //    //ユニティちゃんのAnimatorにアクセスする
    //    animator = GetComponent<Animator>();
    //    //ユニティちゃんの現在より少し前の位置を保存
    //    playerPos = transform.position;
    //}

    //void Update()
    //{
    //    GetInputKey();
    //    ChangeState();
    //    ChangeAnimation();
    //    Move();
    //}


    //void GetInputKey()
    //{
    //    //A・Dキー、←→キーで横移動
    //    float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

    //    //W・Sキー、↑↓キーで前後移動
    //    float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

    //    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
    //    {
    //        key = 1;
    //    }

    //    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        key = -1;
    //    }


    //}

    //void ChangeState()
    //{
    //    if (Ground)
    //    {
    //        if (key != 0)
    //        {
    //            state = "RUN";
    //        }
    //        else
    //        {
    //            state = "IDLE";
    //        }
    //    }
    //    else
    //    {
    //        state = "JUMP";
    //    }
    //}

    //void ChangeAnimation()
    //{
    //    if (prevState != state)
    //    {
    //        switch (state)
    //        {
    //            case "JUMP":
    //                animator.SetBool("Jumping", true);
    //                animator.SetBool("Running", false);
    //                animator.SetBool("Idle", false);
    //                break;
    //            case "RUN":
    //                animator.SetBool("Jumping", false);
    //                animator.SetBool("Running", true);
    //                animator.SetBool("Idle", false);
    //                break;
    //            default:
    //                animator.SetBool("Jumping", false);
    //                animator.SetBool("Running", false);
    //                animator.SetBool("Idle", true);
    //                break;
    //        }
    //        prevState = state;
    //    }
    //}

    //void Move()
    //{
    //    if (Ground)
    //    {
    //        if (Input.GetButton("Jump"))
    //        {
    //            //jumpForceの分だけ上方に力がかかる
    //            rb.AddForce(transform.up * jumpForce);
    //            Ground = false;
    //        }

    //    }

    //    //現在の位置＋入力した数値の場所に移動する
    //    rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

    //    //ユニティちゃんの最新の位置から少し前の位置を引いて方向を割り出す
    //    Vector3 direction = transform.position - playerPos;

    //    //移動距離が少しでもあった場合に方向転換
    //    if (direction.magnitude >= 0.01f)
    //    {
    //        //directionのX軸とZ軸の方向を向かせる
    //        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    }
    //    else
    //    {
    //        key = 0;
    //    }

    //    //ユニティちゃんの位置を更新する
    //    playerPos = transform.position;

    //}

    ////ジャンプ後、Planeに接触した時に接触判定をtrueに戻す
    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.tag == "Ground")
    //    {
    //        if (!Ground)
    //            Ground = true;
    //    }
    //}
}
