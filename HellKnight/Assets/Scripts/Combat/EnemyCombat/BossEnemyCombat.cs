using System.Collections;
using System.Collections.Generic;
using Testing;
using UnityEngine;

[TestMonoBehaviour]
public class BossEnemyCombat : RangedEnemyBaseCombat
{
    public const float FIRE_ANGLE = 30f;

    protected override void ExecuteAttack()
    {
        Vector2 dir = Vector2.right;
        for(int i = 0; i < 12; ++i)
        {
            FireAtDirection(GetProjectile(), dir);
            dir = dir.Rotate(FIRE_ANGLE);
        }
    }

    protected void FireAtDirection(Projectile p, Vector3 dir)
    {
        p.TargetDirection = new Vector3(dir.x, dir.y, 0);
    }


}

