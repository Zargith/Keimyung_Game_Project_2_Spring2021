public class Spell
{
    public enum ReloadType
    {
        USAGE,
        COOLDOWN
    }
    public InputAction.Spell _type { get; private set; }

    public ReloadType _reloadType { get; private set; }

    public int _reloadData { get; private set; }

    private int _actualReloadData;

    public bool activated { get; private set; }
    public bool _instant { get; private set; }

    public Spell(InputAction.Spell type, ReloadType reloadType, int reloadData, bool instant)
    {
        _type = type;
        _reloadType = reloadType;
        _reloadData = reloadData;
        _instant = instant;
        _actualReloadData = reloadData;
        switch (_reloadType)
        {
            case ReloadType.USAGE:
                _actualReloadData = reloadData;
                break;
            case ReloadType.COOLDOWN:
                _actualReloadData = 0;
                break;
        }
    }

    public void Reset()
    {
        _actualReloadData = 0;
    }

    public bool isAvailable()
    {
        switch (_reloadType)
        {
            case ReloadType.USAGE:
                if (_actualReloadData > 0)
                {
                    return true;
                }
                break;
            case ReloadType.COOLDOWN:
                if (_actualReloadData == 0)
                {
                    return (true);
                }
                break;
        }
        return false;
    }

    public bool Activate()
    {
        if (isAvailable())
        {
            activated = true;
            return true;
        }
        return false;
    }

    public void Cancel()
    {
        activated = false;
    }

    public void Deactivate()
    {
        Use();
        activated = false;
    }

    public bool Use()
    {
        switch (_reloadType)
        {
            case ReloadType.USAGE:
                if (_actualReloadData > 0)
                {
                    _actualReloadData--;
                    return true;
                }
                break;
            case ReloadType.COOLDOWN:
                if (_actualReloadData == 0) {
                    _actualReloadData = _reloadData;
                    return (true);
                }
                break;
        }
        return false;
    }

    public void IncreaseTurn()
    {
        if (_reloadType == ReloadType.COOLDOWN && _actualReloadData > 0)
            _actualReloadData--;
    }
        
}
