using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpringTime
{
    public class PlayerShadowController : MonoBehaviour
    {
        public float WindForce = 20f;
        public float MaxVelocity = 5f;

        private Rigidbody2D _rb;
        private CommandType _executingCommand = CommandType.Empty;
        private List<CommandType> _cmdList;
        private List<float> _cmdTimesList;
        private float _cmdTimer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _cmdList = CommandRecorder.CR.GetPrevCmdList();
            _cmdTimesList = CommandRecorder.CR.GetPrevCmdTimesList();

            _cmdTimer = _cmdTimesList[0];
            _executingCommand = _cmdList[0];
        }

        private void Update()
        {
            if (GameManager.GM.State != GameManager.GameState.Record) return;
            _fetchCommand();
            _executeCommand();
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, MaxVelocity);
        }

        public void ChangeExecutingCommand(CommandType cmd)
        {
            _executingCommand = cmd;
        }

        private void _fetchCommand()
        {
            _cmdTimer -= Time.deltaTime;
            if (_cmdTimer <= 0f && _cmdTimesList.Count > 0)
            {
                _cmdTimesList.RemoveAt(0);
                _cmdList.RemoveAt(0);
                if (_cmdTimesList.Count > 0)
                {
                    _cmdTimer = _cmdTimesList[0];
                    _executingCommand = _cmdList[0];
                }
            }
        }

        private void _executeCommand()
        {
            // Execute the current executing command
            switch (_executingCommand)
            {
                case CommandType.Empty:
                    break;
                case CommandType.Up:
                    _rb.AddForce(Vector2.up * WindForce);
                    break;
                case CommandType.Down:
                    _rb.AddForce(Vector2.down * WindForce);
                    break;
                case CommandType.Right:
                    _rb.AddForce(Vector2.right * WindForce);
                    break;
                case CommandType.Left:
                    _rb.AddForce(Vector2.left * WindForce);
                    break;
            }
        }
    }
}

