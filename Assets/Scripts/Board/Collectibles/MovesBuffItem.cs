using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesBuffItem : CollectiblesBase
{
    protected override void AddBuff(Character character)
    {
        character.addOneMoreMove();
    }
}
