using ECS_MONO;
using UnityEngine;

namespace Game.Player.Move
{
    internal sealed class PlayerAirMoveSystemMono : EcsSystemMono<PlayerMovementRuntime, PlayerMovementView, PlayerInput, AirMove>
    {
        private PlayerMovementRuntime _runtime;
        private PlayerMovementView _view;
        private PlayerInput _input;
        
        private Transform _playerTransform;
        private CmdMove _moveSettings;
        
        protected override void Run(EntityMono e, PlayerMovementRuntime c1, PlayerMovementView c2, PlayerInput c3, AirMove c4)
        {
            _runtime = c1;
            _view = c2;
            _input = c3;

            _moveSettings = _view.Data.AirMove;
            _playerTransform = _view.PlayerModel;
            
            Move();
        }
        
        private float airAcceleration = 2.0f; // Air accel
        private float airDecceleration = 2.0f; // Deacceleration experienced when ooposite strafing
        private float sideStrafeAcceleration = 50.0f; // How fast acceleration occurs to get up to sideStrafeSpeed when
        private float sideStrafeSpeed = 1.0f; // What the max speed to generate when side strafing
        
        private Cmd _cmd;

        private void Move()
        {
            float time = Time.deltaTime;

            AirMove(in time);

            _view.CharacterController.Move(_runtime.Velocity * time);
        }
        
        private void SetMovementDir()
        {
            _cmd.forwardMove = _input.AxisRaw.y;
            _cmd.rightMove = _input.AxisRaw.x;
        }

        private void AirMove(in float time)
        {
            Vector3 wishdir;
            float wishvel = airAcceleration;
            float accel;

            SetMovementDir();

            wishdir = new Vector3(_cmd.rightMove, 0, _cmd.forwardMove);
            wishdir = _playerTransform.TransformDirection(wishdir);

            float wishspeed = wishdir.magnitude;
            wishspeed *= _moveSettings.Speed;

            wishdir.Normalize();
           
            float wishspeed2 = wishspeed;
            if (Vector3.Dot(_runtime.Velocity, wishdir) < 0)
                accel = airDecceleration;
            else
                accel = airAcceleration;
           
            if (_cmd.forwardMove == 0 && _cmd.rightMove != 0)
            {
                if (wishspeed > sideStrafeSpeed)
                    wishspeed = sideStrafeSpeed;
                accel = sideStrafeAcceleration;
            }

            Accelerate(wishdir, wishspeed, accel);

            if (_view.Data.AirControl > 0) AirControl(wishdir, wishspeed2, in time);
           
            _runtime.Velocity.y -= _view.Data.Gravity * time;
        }
        
        private void AirControl(Vector3 wishdir, float wishspeed, in float time)
        {
            float zspeed;
            float speed;
            float dot;
            float k;
            
            zspeed = _runtime.Velocity.y;
            _runtime.Velocity.y = 0;
           
            speed = _runtime.Velocity.magnitude;
            _runtime.Velocity.Normalize();

            dot = Vector3.Dot(_runtime.Velocity, wishdir);
            k = 32;
            k *= _view.Data.AirControl * dot * dot * time;
            
            if (dot > 0)
            {
                _runtime.Velocity.x = _runtime.Velocity.x * speed + wishdir.x * k;
                _runtime.Velocity.y = _runtime.Velocity.y * speed + wishdir.y * k;
                _runtime.Velocity.z = _runtime.Velocity.z * speed + wishdir.z * k;

                _runtime.Velocity.Normalize();
            }
            
            _runtime.Velocity.x *= speed;
            _runtime.Velocity.y = zspeed; // Note this line
            _runtime.Velocity.z *= speed;
        }

        private void Accelerate(Vector3 wishdir, float wishspeed, float accel)
        {
            float addspeed;
            float accelspeed;
            float currentspeed;
            
            if (_view.Data.EnableBhop)
            {
                currentspeed = Vector3.Dot(_runtime.Velocity, wishdir); //Бхоп
            }
            else
            {
                currentspeed = _runtime.Velocity.magnitude;
            }

            addspeed = wishspeed - currentspeed;
            if (addspeed <= 0)
                return;
            accelspeed = accel * Time.deltaTime * wishspeed;
            if (accelspeed > addspeed)
                accelspeed = addspeed;

            _runtime.Velocity.x += accelspeed * wishdir.x;
            _runtime.Velocity.z += accelspeed * wishdir.z;
        }
    }
}