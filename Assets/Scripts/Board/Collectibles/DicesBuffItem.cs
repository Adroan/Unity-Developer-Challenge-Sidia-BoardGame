using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicesBuffItem : CollectiblesBase
{
    protected override void AddBuff(Character character)
    {
        character.addOneMoreDice();
    }
}
