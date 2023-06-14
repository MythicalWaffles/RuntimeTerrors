using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectiles
{
    // Start is called before the first frame update
    public override float GetSpeed()
    {
        // Blinky does 0.5 damage, the most of the ghosts.
        return 20;
    }
    public override float GetDamage()
    {
        // Blinky does 0.5 damage, the most of the ghosts.
        return 20f;
    }
}
