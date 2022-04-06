using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to enemies, inanimate damage sources (e.g. projectiles) and environmental hazards (e.g. spikes)
//Provides information to the player about damage taken
public class HazardStats : MonoBehaviour
{
    public float damage = 5;
    public float knockback = 10;
}
