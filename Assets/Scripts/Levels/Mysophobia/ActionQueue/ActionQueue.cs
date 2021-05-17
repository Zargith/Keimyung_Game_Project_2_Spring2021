using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : PositionableGraphic
{
    private Queue<EnvironmentVirus.Type> _queue;

    private Vector2 _queueOriginPos;

    public ActionQueue(PositionProvider pp) : base(pp) {
        ReceivePrefab("ActionQueue");
        _queue = new Queue<EnvironmentVirus.Type>();
        _queueOriginPos = new Vector2(pp.Middle.x - pp.MapSize.x, pp.Middle.y + pp.MapSize.y * 0.6f);
    }
    public override void Draw()
    {
        Instantiate(GetPrefab("CoronaCard"), _queueOriginPos, Quaternion.identity);
    }

}
