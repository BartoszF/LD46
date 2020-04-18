﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeltItem", menuName = "LD46/BeltItem", order = 1)]
public class BeltItemAsset : ScriptableObject
{
    public string name;
    public Sprite sprite;

    public GameObject InstantiateGO() {
        GameObject go = new GameObject();

        go.name = name;

        SpriteRenderer rend = go.AddComponent<SpriteRenderer>();
        rend.sprite = sprite;
        rend.sortingOrder = 150;
        BoxCollider2D bc = go.AddComponent<BoxCollider2D>();
        bc.size = new Vector2(0.4f,0.4f);

        Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        go.AddComponent<BeltItem>();

        return go;
    }
}