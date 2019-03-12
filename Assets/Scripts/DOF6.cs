
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;




public class DOF6 : MonoBehaviour
{
    #region Private Variables
    private MLInputController _controller;

    private static int BUTTON_ACTIVE = 1;
    private static int BUTTON_INACTIVE = 0;
    private static Color BUTTON_ACTIVE_COLOR = Color.blue;
    private static Color BUTTON_INACTIVE_COLOR = Color.white;
    private Renderer _renderer;
    private bool _first = true;
    private bool _active = false;
    #endregion

    #region Unity Methods
    void Start()
    {
        //Start receiving input by the Control
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        _renderer = GetComponent<Renderer>();
    }

    void OnDestroy()
    {
        //Stop receiving input by the Control
        MLInput.Stop();
    }

    void Update()
    {
        //Attach the Beam GameObject to the Control
        transform.position = _controller.Position;
        transform.rotation = _controller.Orientation;


        var button = (int)MLInputControllerButton.Bumper;
        var buttonState = _controller.State.ButtonState[button];

        if (_first && buttonState == BUTTON_ACTIVE)
        {
            _first = false;
            _active = false;
        }
        if (_first && buttonState == BUTTON_INACTIVE)
        {
            _first = false;
            _active = true;
        }
        if (!_active && buttonState == BUTTON_ACTIVE)
        {
            _active = true;
            _renderer.material.color = BUTTON_ACTIVE_COLOR;
        }
        if (_active && buttonState == BUTTON_INACTIVE)
        {
            _active = false;
            _renderer.material.color = BUTTON_INACTIVE_COLOR;
        }

    }
    #endregion
}

