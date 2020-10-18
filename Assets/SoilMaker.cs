using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class SoilMaker : MonoBehaviour, IPutable
{
    public SpriteRenderer SpriteRenderer;
    public Sprite OnWetSprite;
    public Sprite OnDrySprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWet()
    {
        if (OnWetSprite && SpriteRenderer)
        {
            SpriteRenderer.sprite = OnWetSprite;
        }
    }

    public void OnDry()
    {
        if (OnDrySprite && SpriteRenderer)
        {
            SpriteRenderer.sprite = OnDrySprite;
        }
    }
}
