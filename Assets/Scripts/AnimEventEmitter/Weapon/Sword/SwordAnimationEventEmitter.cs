using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Isekai.Events
{
    public class SwordAnimationEventEmitter : MonoBehaviour
    {
        public UnityEvent OnAttackStartTrigger;
        public UnityEvent OnAttackStopTrigger;
        public UnityEvent OnAttackStartMoveTrigger;
        public UnityEvent OnAttackStopMoveTrigger;
        public UnityEvent OnAnimationCompleteTrigger;

        public void TriggerOnAttackStart()
        {
            OnAttackStartTrigger?.Invoke();
        }
        public void TriggerOnAttackStop()
        {
            OnAttackStopTrigger?.Invoke();
        }
        public void TriggerOnAttackStartMove()
        {
            OnAttackStartMoveTrigger?.Invoke();
        }
        public void TriggerOnAttackStopMove()
        {
            OnAttackStopMoveTrigger?.Invoke();
        }
        public void TriggerOnAnimationComplete()
        {
            OnAnimationCompleteTrigger?.Invoke();
        }
    }

}
