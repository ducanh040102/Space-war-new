using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : Bullet
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CrossBoarderBackToPool();
    }

    public void SetDebrisSprite(Sprite setSprite)
    {
        Debug.Log(setSprite.ToString());
        if (spriteRenderer == null)
            return;
        spriteRenderer.sprite = setSprite;
    }
}
