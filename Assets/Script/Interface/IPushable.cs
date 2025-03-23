using UnityEngine;

interface IPushable
{
    void StartPushCoroutine(Vector2 direction,float distance);
}
