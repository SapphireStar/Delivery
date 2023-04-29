using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Datas
{
    [CreateAssetMenu(fileName = "BaseWeaponData", menuName = "Data/Weapons/BaseWeaponData", order = 2)]
    public class BaseWeaponData : BaseData
    {
        public float WeaponDamage;
        public float CombatBuffer;
        public float[] movementSpeed;
    }

}
