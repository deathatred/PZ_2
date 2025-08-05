using UnityEngine;

public class DestroyGameObjectAction : ActionBase
{
    public override void Execute()
    {
        Destroy(gameObject);
    }
}
