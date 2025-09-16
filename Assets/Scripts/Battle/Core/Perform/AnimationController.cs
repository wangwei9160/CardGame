using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void Update()
    {
        BattleManager.Instance?.BaseBattlePlayer?.PerformModule.PlayNextAnimation();
    }
}