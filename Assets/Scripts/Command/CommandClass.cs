using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommandPattern
{
    public abstract class Command
    {
        public abstract void Execute(Transform actorTransform, Command command); 


        public virtual void Undo(Transform actorTransform) { }

        public abstract class MoveCommand : Command
        {

            protected float moveDistance = 1f;
            protected abstract void Move(Transform actorTransform);

            protected virtual bool AtBounds(Transform actorTransform, Vector3 direction)
            {
                int bound = actorTransform.gameObject.GetComponent<InputHandler>().MovementRange;
                Vector3 newPos = actorTransform.position + direction * moveDistance;
                int x = (int)newPos.x;
                int z = (int)newPos.z;
                if (x > bound || x < -bound) return true;
                if (z > bound || z < -bound) return true;
                return false;
            }

        }

        public class MoveForward : MoveCommand
        {
            public override void Execute(Transform actorTransform, Command command)
            {
                //check if we are at a bound
                if (AtBounds(actorTransform, actorTransform.forward)) return;

                Move(actorTransform);
                actorTransform.gameObject.GetComponent<InputHandler>().previousCommands.Push(command);
            }

            public override void Undo(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.forward * -moveDistance);
            }

            protected override void Move(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.forward * moveDistance);
            }

        }
        public class MoveBackward : MoveCommand
        {
            public override void Execute(Transform actorTransform, Command command)
            {
                //check if we are at a bound
                if (AtBounds(actorTransform, -actorTransform.forward)) return;

                Move(actorTransform);
                actorTransform.gameObject.GetComponent<InputHandler>().previousCommands.Push(command);
            }

            public override void Undo(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.forward * moveDistance);
            }
            protected override void Move(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.forward * -moveDistance);
            }

        }
        public class MoveLeft : MoveCommand
        {
            public override void Execute(Transform actorTransform, Command command)
            {
                //check if we are at a bound
                if (AtBounds(actorTransform, -actorTransform.right)) return;

                Move(actorTransform);
                actorTransform.gameObject.GetComponent<InputHandler>().previousCommands.Push(command);
            }

            public override void Undo(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.right * moveDistance);
            }
            protected override void Move(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.right * -moveDistance);
            }

        }

        public class MoveRight : MoveCommand
        {
            public override void Execute(Transform actorTransform, Command command)
            {
                //check if we are at a bound
                if (AtBounds(actorTransform, actorTransform.right)) return;

                Move(actorTransform);
                actorTransform.gameObject.GetComponent<InputHandler>().previousCommands.Push(command);
            }

            public override void Undo(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.right * -moveDistance);
            }

            protected override void Move(Transform actorTransform)
            {
                actorTransform.Translate(actorTransform.right * moveDistance);
            }

        }

        public class UndoCommand : Command
        {
            public override void Execute(Transform actorTransform, Command command)
            {

                Stack<Command> actorCommandStack = actorTransform.gameObject.GetComponent<InputHandler>().previousCommands;
                if (actorCommandStack.Count < 1) return;

                Command lastCommand = actorCommandStack.Pop();  //stack peek ileri
                lastCommand?.Undo(actorTransform);

            }

        }

    }
}