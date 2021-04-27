using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BTtest : MonoBehaviour
{
    [SerializeField] private Text myText;

    private int pushed = 0;

    private void Start()
    {
        InputSystem.AddDevice<myDevice>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            pushed += 1;
            myText.text = pushed.ToString();

            Debug.Log("An Input Detected");
        }
    }
}
