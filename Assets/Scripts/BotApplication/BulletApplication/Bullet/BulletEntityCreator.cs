using UnityEngine;

namespace BattleCoder.GameObject.BotApplication.BulletApplication.Bullet
{
    public class BulletEntityCreator : IBulletEntityCreator
    {
        readonly BulletEntity bulletPrefab;
        readonly int layer;

        public BulletEntityCreator(BulletEntity bulletPrefab, int layer)
        {
            this.bulletPrefab = bulletPrefab;
            this.layer = layer;
        }

        public BulletEntity Create()
        {
            var bulletEntity = Object.Instantiate(bulletPrefab);
            bulletEntity.gameObject.layer = layer;
            return bulletEntity;
        }
    }
}