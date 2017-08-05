using UnityEngine;

public class InputManager
{
    private const string AXIS_VERTICAL = "Vertical";
    private const string AXIS_HORIZONTAL = "Horizontal";

    private const string MOUSE_HORIZONTAL = "Mouse X";
    private const string MOUSE_VERTICAL = "Mouse Y";

    /// <summary>
    /// Setup Input:
    /// Name:       ControllerXView
    /// Gravity     0
    /// Dead        0.2
    /// Sensitivity 1
    /// </summary>
    private const string CONTROLLER_HORIZONTAL = "ControllerXView";

    /// <summary>
    /// Setup Input:
    /// Name:       ControllerYView
    /// Gravity     0
    /// Dead        0.2
    /// Sensitivity 1
    /// </summary>
    private const string CONTROLLER_VERTICAL = "ControllerYView";

    private const string BUTTON_JUMP = "Jump";

    public static float GetVertical()
    {
        return Input.GetAxis(AXIS_VERTICAL);
    }

    public static float GetHorizontal()
    {
        return Input.GetAxis(AXIS_HORIZONTAL);
    }

    public static bool GetJump(ButtonState state)
    {
        switch (state)
        {
            case ButtonState.Pressed:
                return Input.GetButton(BUTTON_JUMP);

            case ButtonState.OnPress:
                return Input.GetButtonDown(BUTTON_JUMP);

            case ButtonState.OnRelease:
                return Input.GetButtonUp(BUTTON_JUMP);

            default:
                return false;
        }
    }

    public static float GetHorizontalLook()
    {
        var value = Input.GetAxis(MOUSE_HORIZONTAL);
        value += Input.GetAxis(CONTROLLER_HORIZONTAL);
        return Mathf.Clamp(value, -1f, 1f);
    }

    public static float GetVerticalLook(bool inverted = false)
    {
        var value = Input.GetAxis(MOUSE_VERTICAL);
        value += Input.GetAxis(CONTROLLER_VERTICAL);
        value = Mathf.Clamp(value, -1f, 1f);
        if (!inverted)
        {
            value = 0 - value;
        }
        return value;
    }

    public static float GetZoom()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }
}
