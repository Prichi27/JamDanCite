﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power", menuName = "Power", order = 0)]
public class Power : ScriptableObject {
    public string powerName;
    public Sprite sprite;
    public float projectileSpeed;
    public float damage;
    public float damageRadius;
    public float manaDrain;
    public float attackSpeed;
    public GameObjectPool particleOnShoot;
    public GameObjectPool particleOnDestroy;
}
