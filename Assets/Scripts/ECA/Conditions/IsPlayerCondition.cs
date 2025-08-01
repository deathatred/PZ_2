using UnityEngine;

public class IsPlayerCondition : Condition
{
    public override bool IsMet(Collider other)
    {
        return other.CompareTag("Player");
    }
}
