using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "Pickup", order = 0)]
public class Pickup : ScriptableObject
{
    public Sprite sprite;
    public GameObjectPool projectilePool;
    public ParticleSystem particleOnPickup;
}
