using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Canvas/ButtonSummary/Button").GetComponent<Button>();
        //ƒ{ƒ^ƒ“‚ª‘I‘ğ‚³‚ê‚½ó‘Ô‚É‚È‚é
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
