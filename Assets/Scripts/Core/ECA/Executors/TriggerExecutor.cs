using UnityEngine;

public class TriggerExecutor : Executor
{
    private void OnTriggerEnter(Collider other)
    {
        if (AreConditionsMet(other))
        {
            ExecuteActions();
        }
    }
}
