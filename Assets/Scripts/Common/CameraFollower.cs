using UnityEngine;

namespace BattleCoder.Common
{
    public class CameraFollower : MonoBehaviour
    {
        Transform targetTransform;
        Vector2 offset;

        //追従するプレイヤーの座標を設定
        public void SetPlayerPosition(Transform targetTransform)
        {
            this.targetTransform = targetTransform;

            var position = transform.position;
            offset.x = (1280 / 2 - 854 / 2) / 2;
            offset.y = 0;
        }

        void LateUpdate()
        {
            if (targetTransform == null) return;
            var x = targetTransform.position.x + offset.x;
            var y = targetTransform.position.y + offset.y;
            var transform1 = transform.transform;
            transform1.position = new Vector3(x, y, transform1.position.z);
        }
    }
}