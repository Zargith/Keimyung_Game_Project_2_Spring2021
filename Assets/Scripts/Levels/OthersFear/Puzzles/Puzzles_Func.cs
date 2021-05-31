using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzles_Func
{
  public static void FlipTarget(GameObject target, Vector2 flip_by)
    {
        var tmp = target.transform.localScale;
        tmp.x *= flip_by.x;
        tmp.y *= flip_by.y;
        target.transform.localScale = tmp;
    }
}
