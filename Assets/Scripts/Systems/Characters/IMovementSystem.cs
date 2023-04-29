using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems
{
    public interface IMovementSystem:ISystem
    {
        public void ProcessMove(Vector2 direction);
    }
}

