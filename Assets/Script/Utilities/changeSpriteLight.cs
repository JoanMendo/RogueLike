using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class changeSpriteLight : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //change the sprite of the current light2D in sprite type
        
        light2D.lightCookieSprite = spriteRenderer.sprite;

    }
}
