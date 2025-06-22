using Unity.VisualScripting;
using UnityEngine;

public class WeedBrownie : Consumable
{
    protected override void stats()
    {
        elijah.updateHunger(20);
        elijah.updateHappiness(50);
        elijah.updateAddiction(20);
    }
}
