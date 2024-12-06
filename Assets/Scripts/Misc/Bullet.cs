using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected virtual void CrossBoarderBackToPool()
    {
        if (!ScreenBoundary.Instance.IsInsideScreen(transform.position))
        {
            BackToPool();
        }
    }

    public void BackToPool()
    {
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}
