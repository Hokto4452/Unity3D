using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessCharaAnimEvent : MonoBehaviour
{
    //private PlayerController characterScript;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    characterScript = GetComponent<PlayerController>();
    //}

    //public void EndDamage()
    //{
    //    characterScript.SetState(characterScript.MyState.Normal);
    //}

    private PlayerController characterScript;

    private void Start()
    {
        characterScript = GetComponent<PlayerController>();
    }

    public void EndDamage()
    {
        //characterScript.(PlayerController.MyState.Normal);
    }
}
