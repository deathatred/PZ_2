using Mono.Cecil;
using UnityEngine;

public class CollectAction : ActionBase
{
    private CollectibleCondition _condition;
    public override void Execute()
    {
        if (TryGetComponent<CollectibleCondition>(out var condition))
        {
            _condition = condition;
        }
        else
        {
            print("null");
        }
        ICollectible collectible = condition.GetTarget().Target;
        if (collectible != null)
        {
            collectible.Collect();

        }
    }
}
