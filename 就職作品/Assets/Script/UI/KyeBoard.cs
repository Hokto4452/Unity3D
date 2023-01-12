using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KyeBoard : MonoBehaviour
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("UI/MapCanvas/ButtonSummry/Button").GetComponent<Button>();
        button.Select();                            //ƒ{ƒ^ƒ“‚ª‘I‘ğ‚³‚ê‚½ó‘Ô‚É‚È‚é
    }

    // Update is called once per frame
    void Update()
    {

    }
}
