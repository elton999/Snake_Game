using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [NonSerializedAttribute] public int index;
    [NonSerializedAttribute] public SpawnWalls walls;
    SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update() {
        if(Time.time % 1 > 0.5f)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
        else
           sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.7f); 
        if(walls.tiles[index] == 1){
            DestroyObject(gameObject);
        }
    }
}
