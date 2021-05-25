using UnityEngine;

public class InputManager
{
    public void GetInput(InputAction action)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            action.Set(InputAction.Type.MOVE, (int)InputAction.Direction.DOWN);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            action.Set(InputAction.Type.MOVE, (int)InputAction.Direction.UP);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            action.Set(InputAction.Type.MOVE, (int)InputAction.Direction.LEFT);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            action.Set(InputAction.Type.MOVE, (int)InputAction.Direction.RIGHT);
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            action.Set(InputAction.Type.SPELL, (int)InputAction.Spell.ACCELERATION);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            action.Set(InputAction.Type.SPELL, (int)InputAction.Spell.DELETE_VIRUS);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            action.Set(InputAction.Type.RETRY);
        else
            action.Set(InputAction.Type.NONE);
    }
}
