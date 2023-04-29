using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Events
{
    public class PlayerAnimationEventEmitter : MonoBehaviour
    {
        public UnityEvent OnDodgeAnimationCompleteTrigger;
        public void TriggerOnDodgeAnimationComplete()
        {
            OnDodgeAnimationCompleteTrigger?.Invoke();
        }

    }

}
