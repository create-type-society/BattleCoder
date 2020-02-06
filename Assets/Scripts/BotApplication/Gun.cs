using System.Collections.Generic;
using System.Linq;
using BattleCoder.BotApplication.BulletApplication.Bullet;
using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.BotApplication
{
    //砲台を表すクラス
    public class Gun
    {
        readonly SoundManager soundManager;
        readonly IBulletEntityCreator bulletEntityCreator;

        List<BattleCoder.BotApplication.BulletApplication.BulletApplication> bulletApplicationList =
            new List<BattleCoder.BotApplication.BulletApplication.BulletApplication>();

        const int maxCoolDownTime = 20;
        int coolDownTime = 0;
        readonly bool isNonCoolDownTime;

        public Gun(SoundManager soundManager, IBulletEntityCreator bulletEntityCreator, bool isNonCoolDownTime)
        {
            this.soundManager = soundManager;
            this.bulletEntityCreator = bulletEntityCreator;
            this.isNonCoolDownTime = isNonCoolDownTime;
        }

        public bool Shot(Vector3 shotPosition, float shotRotation)
        {
            if (isNonCoolDownTime == false && coolDownTime != 0) return false;
            coolDownTime = maxCoolDownTime;
            soundManager.MakeFiringSound();
            var bulletEntity = bulletEntityCreator.Create();
            bulletEntity.transform.position = shotPosition;
            bulletEntity.transform.rotation = Quaternion.Euler(0, 0, shotRotation);
            var bulletApplication =
                new BattleCoder.BotApplication.BulletApplication.BulletApplication(bulletEntity,
                    CalcRotationVector(bulletEntity, 2f));
            bulletApplicationList.Add(bulletApplication);
            return true;
        }

        public void Update()
        {
            if (coolDownTime > 0) coolDownTime--;
            bulletApplicationList.ForEach(x => x.Update());
            bulletApplicationList = bulletApplicationList.Where(x =>
            {
                if (x.DeleteFlag) x.Delete();
                return x.DeleteFlag == false;
            }).ToList();
        }

        Vector3 CalcRotationVector(BulletEntity bulletEntity, float speed)
        {
            var r = bulletEntity.transform.rotation.eulerAngles.z;
            var x = -Mathf.Sin(ToRadians(r));
            var y = Mathf.Cos(ToRadians(r));
            return new Vector3(x, y) * speed;
        }

        float ToRadians(float angle)
        {
            return Mathf.PI / 180f * angle;
        }
    }
}