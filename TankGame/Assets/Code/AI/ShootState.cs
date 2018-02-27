using UnityEngine;

namespace TankGame.AI
{
    public class ShootState : AIStateBase
    {
        public float SqrShootingDistance
        {
            get { return Owner.ShootingDistance * Owner.ShootingDistance; }
        }

        public ShootState(EnemyUnit owner)
            : base(owner, AIStateType.Shoot)
        {
            AddTransition(AIStateType.Patrol);
            AddTransition(AIStateType.FollowTarget);
 
        }

        public override void Update()
        {
            // 1. Should we change the state?
            //   1.1 If yes, change state and return.
            Debug.Log("Ammutaan");
            if (!ChangeState())
            {
                Owner.Weapon.Shoot();
                Owner.Mover.Move(Owner.transform.forward);
                Owner.Mover.Turn(Owner.Target.transform.position);
            }
        }

        private bool ChangeState()
        {
            // 1. Are player at detection range/dead?
            //if yes go Patrol

            if (Owner.Target.Health.CurrentHealth <= 0)
            {
                Owner.Target = null;
                return Owner.PerformTransition(AIStateType.Patrol);
            }
               

            //2. Is player on shooting range and alive?
            //if not on range and alive go followstate.

            Vector3 toPlayerVector =
                Owner.transform.position - Owner.Target.transform.position;
            float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;

            if (sqrDistanceToPlayer > SqrShootingDistance)
            {
                return Owner.PerformTransition(AIStateType.FollowTarget);
            }
            //return false if no Changing State
            return false;
        }
    }
}
