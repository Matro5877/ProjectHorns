using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardSign : MonoBehaviour
{
    public SpriteRenderer itSelf;
    public bool keyBoardControls;
    public bool controllerControls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyBoardControls = ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)
        || Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Keypad5)
        || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.W)
        || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Numlock) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Keypad0)
        || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.F3) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.V)
        || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.F6)));
        
        controllerControls = ((Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxis("HorizontalMenu") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxis("HorizontalMenu") < 0
        || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxis("VerticalMenu") > 0 || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton1)
        || Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetAxis("ZR") > 0 || Input.GetAxis("ZL") > 0 || Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.RightControl)
        || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5)));
    
        if (keyBoardControls)
        {
            Debug.Log("KeyBoard");
            itSelf.enabled = true;
        }
        if (controllerControls)
        {
            Debug.Log("Controller");
            itSelf.enabled = false;
        }
    }
}
