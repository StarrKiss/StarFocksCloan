using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectData
{
    public enum interactionType 
    {
        hurt,
        heal,
        bounce,
        nothing
    }

    public interactionType main = interactionType.nothing;

    public int healthAmount = 0;
    

}
