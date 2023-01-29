using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbackjump : MonoBehaviour
{
    private Rigidbody _rigidbody;   //ï®óùîªíË
    //bool Ground = true;
    public float jumpForce = 600.0f;
    public float backSpeed = 5f;
    private float jumpInterval;
    private float jumpReloadInterval;

    public int jumpCount = 1;
    public bool notJump;
    bool haveJump;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        notJump = false;
        haveJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(haveJump);
        if (Input.GetKey(KeyCode.Space))
        {
            if (haveJump == true)
            {
                //jumpForceÇÃï™ÇæÇØè„ï˚Ç…óÕÇ™Ç©Ç©ÇÈ
                _rigidbody.AddForce(transform.up * jumpForce);
            }
            else if(haveJump == false)
            {
                _rigidbody.AddForce(-transform.forward * backSpeed);
            }
            //haveJump = false;
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
    }
}
