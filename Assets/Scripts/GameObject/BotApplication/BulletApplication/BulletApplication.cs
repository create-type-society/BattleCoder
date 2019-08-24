//Bulletを外部から簡単に制御できるようにするクラス

using UnityEngine;

public class BulletApplication
{
    BulletEntity bulletEntity;
    Vector3 addPos;

    public BulletApplication(BulletEntity bulletEntity, Vector3 addPos)
    {
        this.bulletEntity = bulletEntity;
        this.addPos = addPos;
    }

    //bulletの更新をする
    public void Update()
    {
        bulletEntity.transform.position += addPos;
    }
}