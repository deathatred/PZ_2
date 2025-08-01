
using UnityEngine;

public class Executor : MonoBehaviour
{
    [SerializeField] protected Condition[] _conditions;
    [SerializeField] protected ActionBase[] _actions;

    protected bool AreConditionsMet(Collider other)
    {
        foreach (var condition in _conditions)
        {
            if (!condition.IsMet(other))
            {
                return false;
            }
        }
        return true;
    }
    protected void ExecuteActions()
    {
        foreach (var action in _actions)
        {
            action.Execute();
        }
    }
}
