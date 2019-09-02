using UnityEngine;

public class MeleeAttackApplication
{
    MeleeAttackEntity meleeAttackEntity;
    SoundManager soundManager;

    public MeleeAttackApplication(MeleeAttackEntity meleeAttackEntity, SoundManager soundManager)
    {
        this.meleeAttackEntity = meleeAttackEntity;
        this.soundManager = soundManager;
    }

    public void MeleeAttack(Vector3 position, Direction direction)
    {
        meleeAttackEntity.MeleeAttack(position, direction);
        soundManager.MakeMeleeSound();
    }
}
