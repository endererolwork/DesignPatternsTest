using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public abstract class Command
    {
        public abstract void Execute(Transform actorTransform, Command command);

        //


        public virtual void Undo(Transform actorTransform) { }

        public virtual void Redo(Transform actorTransform) { }

        public abstract class MoveCommand : Command
        {



        }

    }
}