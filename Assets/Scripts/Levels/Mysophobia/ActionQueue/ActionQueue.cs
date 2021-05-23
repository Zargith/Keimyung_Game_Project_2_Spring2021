using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActionQueue : PositionableGraphic
{
    private const int DEFAULT_ACTION_SIZE = 5;

    private int _actionsSize;

    private int _maxActions;

    private List<EnvironmentVirus.Type> _actions;

    private List<GameObject> _actionObjects;

    private int _index;

    private Vector2 _queueOriginPos;
    public override void Init(PositionProvider pp)
    {
        _pp = pp;

        ReceivePrefab("ActionQueue");

        _queueOriginPos = new Vector2(pp.Middle.x - pp.MapSize.x * 1.2f, pp.Middle.y + pp.MapSize.y * 0.6f);
        _index = 0;
        _actionsSize = DEFAULT_ACTION_SIZE;
    }
    public override void Draw()
    {
        Random.InitState((int)DateTime.Now.Ticks);

        for (int i = 0; i < _maxActions; i++)
        {
            _actions.Add(GetRandomType());
        }
        for (int i = 0; i < _actionsSize; i++)
        {
            _actionObjects.Add(Instantiate(GetPrefab("Card"), new Vector2(_queueOriginPos.x + i * 1.2f, _queueOriginPos.y), Quaternion.identity));
        }
        ReloadDisplay();
        _actionObjects[0].transform.Find("Frame").gameObject.SetActive(true);
    }

    public void SetMaxAction(int maxActions)
    {
        _maxActions = maxActions + _actionsSize;

        _actions = new List<EnvironmentVirus.Type>(_maxActions);
        _actionObjects = new List<GameObject>(_maxActions);
    }

    public void SetActionSize(int actionsSize)
    {
        int initialMaxActions = _maxActions - _actionsSize;

        _actionsSize = actionsSize;
        SetMaxAction(initialMaxActions);
    }
    public EnvironmentVirus.Type Peek()
    {
        return (_actions[_index]);
    }
    public void Next()
    {
        _index++;
        ReloadDisplay();
    }

    private EnvironmentVirus.Type GetRandomType()
    {
        return (EnvironmentVirus.Type)Random.Range(1, 4);
    }

    private void ReloadDisplay()
    {
        for (int i = 0; i < _actionsSize; i++)
         {
             _actionObjects[i].GetComponentInChildren<SpriteRenderer>().color = GetColorFromType(_actions[_index + i]);
         }
    }

    private Color GetColorFromType(EnvironmentVirus.Type type)
    {
        return type switch
        {
            EnvironmentVirus.Type.CORONA => Color.red,
            EnvironmentVirus.Type.FLU => Color.blue,
            EnvironmentVirus.Type.PLAGUE => Color.green,
            _ => throw new Exception("Invalid environment virus type (or installer which is forbidden here): " + type),
        };
    }
}
