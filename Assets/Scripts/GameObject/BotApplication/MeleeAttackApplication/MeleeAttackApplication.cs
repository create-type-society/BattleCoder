using UnityEngine;

public class MeleeAttackApplication
{
    MeleeAttackEntity meleeAttackEntity;
    SoundManager soundManager;
    int coolTime = 0;

    public MeleeAttackApplication(MeleeAttackEntity meleeAttackEntity, SoundManager soundManager)
    {
        this.meleeAttackEntity = meleeAttackEntity;
        this.soundManager = soundManager;
    }

    public void MeleeAttack(Vector3 position, Direction direction)
    {
        if (coolTime > 0) return;
        meleeAttackEntity.MeleeAttack(position, direction);
        soundManager.MakeMeleeSound();
        coolTime = 30;
    }

    public void Update()
    {
        coolTime--;
    }
}