using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffItem : CollectiblesBase
{
    public int damageBuff;
    protected override void AddBuff(Character character)
    {
        character.IncreaseDamage(damageBuff);
    }
}
