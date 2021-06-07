using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : CollectiblesBase
{
    public int healingFactor;
    protected override void AddBuff(Character character)
    {
        character.Healing(healingFactor);
    }
}
