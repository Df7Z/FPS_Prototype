using ECS_MONO;
using UnityEngine;

namespace Game.Player.Move
{
    internal sealed class PlayerGroundMoveSystemMono : EcsSystemMono<PlayerMovementRuntime, PlayerMovementView, PlayerInput, GroundMove>
    {
        private PlayerMovementRuntime _runtime;
        private PlayerMovementView _view;
        private PlayerInput _input;

        private Transform _playerTransform;
        private CmdMove _moveSettings;

        protected override void Run(EntityMono e, PlayerMovementRuntime c1, PlayerMovementView c2, PlayerInput c3, GroundMove c4)
        {
            _runtime = c1;
            _view = c2;
            _input = c3;

            _moveSettings = _view.Data.GroundMove;
            _playerTransform = _view.PlayerModel;
            
            Move();
        }
        
        private bool wishJump = false;
        private Cmd _cmd;
        
        private void Move()
        {
            float time = Time.deltaTime;

            QueueJump();

            GroundMove(in time);

            if (wishJump)
            {
                _runtime.Velocity.y = _view.Data.JumpSpeed;

                wishJump = false;
            }
            
            _view.CharacterController.Move(_runtime.Velocity * time);
        }

        private void SetMovementDir()
        {
            _cmd.forwardMove = _input.AxisRaw.y;
            _cmd.rightMove = _input.AxisRaw.x;
        }
        
        private void QueueJump()
        {
            if (_view.Data.HoldJumpToBhop)
            {
                wishJump = _input.IsJump;
                return;
            }

            if (_input.IsJumpDown && !wishJump)
                wishJump = true;
            if (_input.IsJumpUp)
                wishJump = false;
        }
        
        private void GroundMove(in float time)
        {
            Vector3 wishdir;
            
            if (!wishJump)
                ApplyFriction(in time, 1.0f);
            else
                ApplyFriction(in time, 0);

            SetMovementDir();

            wishdir = new Vector3(_cmd.rightMove, 0, _cmd.forwardMove);
            wishdir = _playerTransform.TransformDirection(wishdir);
            wishdir.Normalize();
            
            var wishspeed = wishdir.magnitude;
            wishspeed *= _moveSettings.Speed;

            Accelerate(wishdir, wishspeed, _moveSettings.Acceleration, time);

            _runtime.Velocity.y = -_view.Data.Gravity * time;
        }
        
        private void ApplyFriction(in float time, float t)
        {
            Vector3 vec = _runtime.Velocity;
            float speed;
            float newspeed;
            float control;
            float drop;

            vec.y = 0.0f;
            speed = vec.magnitude;
            drop = 0.0f;
            
            if (_view.CharacterController.isGrounded)
            {
                control = speed < _moveSettings.Deacceleration ? _moveSettings.Deacceleration : speed;
                drop = control * _view.Data.Friction * time * t;
            }

            newspeed = speed - drop;
            //playerFriction = newspeed;
            if (newspeed < 0)
                newspeed = 0;
            if (speed > 0)
                newspeed /= speed;

            _runtime.Velocity.x *= newspeed;
            _runtime.Velocity.z *= newspeed;
        }

        private void Accelerate(Vector3 wishdir, float wishspeed, float accel, in float time)
        {
            float addspeed;
            float accelspeed;
            float currentspeed;

            if (_view.Data.EnableBhop)
            {
                currentspeed = Vector3.Dot(_runtime.Velocity, wishdir); 
            }
            else
            {
                currentspeed = _runtime.Velocity.magnitude;
            }

            addspeed = wishspeed - currentspeed;
            if (addspeed <= 0)
                return;
            accelspeed = accel * time * wishspeed;
            if (accelspeed > addspeed)
                accelspeed = addspeed;

            _runtime.Velocity.x += accelspeed * wishdir.x;
            _runtime.Velocity.z += accelspeed * wishdir.z;
        }
    }
}