using UnityEngine;

public class TargetComponent
{
    public ICollectible Target { get; private set; }
    public void SetTarget(ICollectible target)
    {
        Target = target;
    }
}
