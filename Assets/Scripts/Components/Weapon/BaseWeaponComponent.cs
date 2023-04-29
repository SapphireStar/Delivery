using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Components
{
    public class BaseWeaponComponent : BaseComponent
    {
        private bool m_isAttacking;
        public bool IsAttacking
        {
            get => m_isAttacking;
            set
            {
                ChangePropertyAndNotify(ref m_isAttacking, value);
            }
        }
        public override void Initialize(BaseComponentSystem system)
        {
            
        }
    }

}
