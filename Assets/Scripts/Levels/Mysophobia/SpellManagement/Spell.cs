using UnityEngine;
using UnityEngine.UI;
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

    private Image _icon;

    private Text _dataDisplay;

    public Spell(InputAction.Spell type, ReloadType reloadType, int reloadData, bool instant)
    {
        _type = type;
        _reloadType = reloadType;
        _reloadData = reloadData;
        _instant = instant;
        _actualReloadData = reloadData;
        _icon = null;
        _dataDisplay = null;
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

    public void AttachDisplay(string containerName)
    {
        GameObject container = GameObject.Find(containerName);
        Image image = container.GetComponentInChildren<Image>();
        Text text = container.GetComponentInChildren<Text>();

        if (image != null)
        {
            _icon = image;
        }
        if (text != null)
        {
            _dataDisplay = text;
            if (_reloadType == ReloadType.COOLDOWN)
                _dataDisplay.text = "";
            else
                _dataDisplay.text = _actualReloadData.ToString();
        }
    }

    public void Reset()
    {
        _actualReloadData = 0;
    }

    public bool IsAvailable()
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
        if (IsAvailable())
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
                    _dataDisplay.text = _actualReloadData.ToString();
                    return true;
                }
                break;
            case ReloadType.COOLDOWN:
                if (_actualReloadData == 0) {
                    _actualReloadData = _reloadData;
                    _dataDisplay.text = _actualReloadData.ToString();
                    _icon.color = Color.black;
                    return true;
                }
                break;
        }
        return false;
    }

    public void IncreaseTurn()
    {
        if (_reloadType == ReloadType.COOLDOWN && _actualReloadData > 0)
        {
            _actualReloadData--;
            if (_actualReloadData == 0)
            {
                _dataDisplay.text = "";
                _icon.color = Color.white;
            } else
            {
                _dataDisplay.text = _actualReloadData.ToString();
            }
        }
    }
        
}
