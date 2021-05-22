using System;

public class InputAction
{
    public enum Type
    {
        NONE,
        MOVE,
        SPELL,
        RETRY
    }

    public enum Direction {
        UP = 0,
        DOWN = 1,
        RIGHT = 2,
        LEFT = 3
    }

    public enum Spell
    {
        ACCELERATION = 4,
        DELETE_VIRUS = 5
    }
    public Type _type { get; private set; }

    private int _data;

    // TODO d�gueulasse � refaire plus tard (need void* data)
    public InputAction() { }
    public InputAction(Type type, int data = 0)
    {
        switch (type)
        {
            case Type.RETRY:
            case Type.NONE:
                break;
            case Type.MOVE:
                if (data < 0 || data > 3)
                    throw new Exception("Data error for move input");
                break;
            case Type.SPELL:
                if (data < 4 || data > 5)
                    throw new Exception("Data error for spell input");
                break;
        }
        _type = type;
        _data = data;
    }

    public void Set(Type type, int data = 0)
    {
        switch (type)
        {
            case Type.RETRY:
            case Type.NONE:
                break;
            case Type.MOVE:
                if (data < 0 || data > 3)
                    throw new Exception("Data error for move input");
                break;
            case Type.SPELL:
                if (data < 4 || data > 5)
                    throw new Exception("Data error for spell input");
                break;
        }
        _type = type;
        _data = data;
    }

    public Direction getDirection()
    {
        if (_type != Type.MOVE)
            throw new Exception("Can't get direction for " + _type + " input");
        return (Direction)_data;
    }

    public Spell getSpell()
    {
        if (_type != Type.SPELL)
            throw new Exception("Can't get spell type for " + _type + " input");
        return (Spell)_data;
    }
}