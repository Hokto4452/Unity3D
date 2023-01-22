using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIRotetaion : MonoBehaviour
{
    public Canvas canvas;

    void LateUpdate()
    {
        canvas.transform.rotation = Camera.main.transform.rotation;
    }
}
