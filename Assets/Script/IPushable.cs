using System.Collections;
using UnityEngine;

interface IPushable
{
    IEnumerator PushBackIEnum(Vector2 direction,float distance);
}
