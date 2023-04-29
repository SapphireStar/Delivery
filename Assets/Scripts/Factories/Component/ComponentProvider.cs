using Isekai.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace Isekai.Factories
{
    public enum EComponent
    {
        //Character
        MovementComponent = 1,
        CombatComponent = 2,
        AnimatorComponent = 3,

        //Weapon
        BaseWeaponComponent = 100,
    }
    public class ComponentProvider
    {
        public static ProfilerMarker GetComponentMarker = new ProfilerMarker("GetComponentMarker");
        public static IComponent GetComponent(EComponent componentName)
        {
            GetComponentMarker.Begin();
            Type type = Type.GetType(string.Format("{0}{1}","Isekai.Components.",componentName.ToString()));
            if(type == null)
            {
                throw new NullReferenceException(string.Format("Class {0} Not Exist", componentName));
            }

            object component = Activator.CreateInstance(type);

            GetComponentMarker.End();
            return component as IComponent;
            
        }
    }
}

