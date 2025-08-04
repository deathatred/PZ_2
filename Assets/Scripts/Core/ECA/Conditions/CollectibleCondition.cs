using UnityEngine;

public class CollectibleCondition : Condition
{
    private TargetComponent target = new TargetComponent();
    public override bool IsMet(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.TryGetComponent<ICollectible>(out var collectible))
            {
                target.SetTarget(this.GetComponent<ICollectible>());
            }
            else
            {
                print("Collectible condition should not be on non-collectible object");
            }
        }
        return other.CompareTag("Player");
    }
    public TargetComponent GetTarget() { return target; }
}
