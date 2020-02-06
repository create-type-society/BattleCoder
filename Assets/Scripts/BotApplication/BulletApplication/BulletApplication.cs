//Bulletを外部から簡単に制御できるようにするクラス

using BattleCoder.BotApplication.BulletApplication.Bullet;
using UnityEngine;

namespace BattleCoder.BotApplication.BulletApplication
{
    public class BulletApplication
    {
        BulletEntity bulletEntity;
        Vector3 addPos;
        public bool DeleteFlag { get; private set; }

        public BulletApplication(BulletEntity bulletEntity, Vector3 addPos)
        {
            this.bulletEntity = bulletEntity;
            this.addPos = addPos;
            this.bulletEntity.RockHitedEvent += (sender, args) => { DeleteFlag = true; };
        }

    
        //bulletの更新をする
        public void Update()
        {
            bulletEntity.transform.position += addPos;
        }

        public void Delete()
        {
            GameObject.Destroy(bulletEntity.gameObject);
        }
    }
}