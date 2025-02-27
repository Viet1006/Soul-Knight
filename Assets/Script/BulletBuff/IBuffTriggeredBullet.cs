using UnityEngine;
// Khi cài đặt interface này buffeffect sẽ kích hoạt theo loại buff đấy khi va chạm
interface IBuffTriggeredBullet
{
    void OnTriggerEnter2D(Collider2D collider);
}