using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackApplication
{
    MeleeAttackEntity meleeAttackEntity;

    public MeleeAttackApplication(MeleeAttackEntity meleeAttackEntity)
    {
        this.meleeAttackEntity = meleeAttackEntity;
    }
    public void MeleeAttack(Vector3 position, Direction direction)
    {
        meleeAttackEntity.MeleeAttack(position, direction);
    }
}
