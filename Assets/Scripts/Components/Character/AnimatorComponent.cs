using Isekai.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Isekai.Components
{
    public enum PlayerAnimLayer
    {
        MoveLayer = 0,
        AttackLayer = 1,
    }
    public class AnimatorComponent : BaseComponent
    {
        private Animator m_animator;
        public Animator AnimController 
        {
            get
            {
                if (m_animator != null)
                {
                    return m_animator;
                }
                return null;
            }

        }

        public override void Initialize(BaseComponentSystem system)
        {
            Type type = system.GetType();
            //使用反射的方式从ComponentSystem中获取Animator
            FieldInfo info = type.GetField("AnimController",BindingFlags.NonPublic|BindingFlags.Instance);
            if (info == null)
            {
                throw new Exception("The ComponentSystem doesn't has Field AnimController. Please check whether the Component System has an Animator or the Animator is named as \"AnimController\"");
            }
            m_animator = info.GetValue(system) as Animator;
            Debug.Log(m_animator);
        }
    }

}
