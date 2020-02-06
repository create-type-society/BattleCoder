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
        List<global::BattleCoder.BotApplication.BulletApplication.BulletApplication> bulletApplicationList = new List<global::BattleCoder.BotApplication.BulletApplication.BulletApplication>();


        public Gun(SoundManager soundManager, IBulletEntityCreator bulletEntityCreator)
        {
            this.soundManager = soundManager;
            this.bulletEntityCreator = bulletEntityCreator;
        }

        public void Shot(Vector3 shotPosition, float shotRotation)
        {
            soundManager.MakeFiringSound();
            var bulletEntity = bulletEntityCreator.Create();
            bulletEntity.transform.position = shotPosition;
            bulletEntity.transform.rotation = Quaternion.Euler(0, 0, shotRotation);
            var bulletApplication = new global::BattleCoder.BotApplication.BulletApplication.BulletApplication(bulletEntity, CalcRotationVector(bulletEntity, 2f));
            bulletApplicationList.Add(bulletApplication);
        }

        public void Update()
        {
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