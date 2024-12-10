using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;


public class Door : Script
{
    public void Open()
    {
        Actor.IsActive = false;
    }
}
