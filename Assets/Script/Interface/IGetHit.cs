using UnityEngine;

interface IGetHit
{
    void GetHit(int damage,BulletElement bulletElements , bool notify = true);
}
