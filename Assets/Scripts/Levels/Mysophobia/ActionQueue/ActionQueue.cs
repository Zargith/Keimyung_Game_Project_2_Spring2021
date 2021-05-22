using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActionQueue : PositionableGraphic
{
    private int _maxActions;

    private const int _actionsSize = 5;

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
    public EnvironmentVirus.Type Peek()
    {
        return (_actions[_index]);
    }
    public void Next()
    {
        _index++;
        ReloadDisplay();
    }

    /*private GameObject GetPrefabFromType(EnvironmentVirus.Type type)
    {
        switch (type)
        {
            case EnvironmentVirus.Type.CORONA:
                return (GetPrefab("CoronaCard"));
            case EnvironmentVirus.Type.FLU:
                return (GetPrefab("FluCard"));
            case EnvironmentVirus.Type.PLAGUE:
                return (GetPrefab("PlagueCard"));
        }
        return null;
    }*/

    private EnvironmentVirus.Type GetRandomType()
    {
        return (EnvironmentVirus.Type)Random.Range(1, 4);
    }

    private void ReloadDisplay()
    {
       // Debug.Log("Display from " + _index + " to " + (_index + _actionsSize));

        for (int i = 0; i < _actionsSize; i++)
         {
             _actionObjects[i].GetComponentInChildren<SpriteRenderer>().color = getColorFromType(_actions[_index + i]);
         }
    }

    private Color getColorFromType(EnvironmentVirus.Type type)
    {
        switch (type)
        {
            case EnvironmentVirus.Type.CORONA:
                return Color.red;
            case EnvironmentVirus.Type.FLU:
                return Color.blue;
            case EnvironmentVirus.Type.PLAGUE:
                return Color.green;
        }
        return Color.black;
    }
}
