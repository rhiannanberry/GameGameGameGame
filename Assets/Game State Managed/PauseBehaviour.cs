using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameState;
public abstract class PauseBehaviour : GameStateBehaviour
{
    protected override GameState _event { get{ return INPAUSE; } }
}
