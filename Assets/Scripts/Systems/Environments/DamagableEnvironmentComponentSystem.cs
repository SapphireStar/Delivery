using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems 
{
    public class DamagableEnvironmentComponentSystem : EnvironmentComponentSystem, IDamagable
    {
        public void ApplyDamage(float damage)
        {
            GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Debug.Log($"Get Damaged {damage}");
        }
    }
}


