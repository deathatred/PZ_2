using UnityEngine;

public class KillPlayerAction : ActionBase
{
    public override void Execute()
    {
        GameEventBus.PlayerDead();
    }
}
