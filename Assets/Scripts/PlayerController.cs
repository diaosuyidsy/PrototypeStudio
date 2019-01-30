using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpringTime
{
    public class PlayerController : MonoBehaviour
    {
        #region Normal Variables
        public float WindForce = 100f;

        private Rigidbody2D _rb;
        #endregion

        #region Command Related Variables
        private enum CommandType
        {
            Empty,
            Up,
            Down,
            Left,
            Right
        }
        // Set the command current command to be something not empty
        private CommandType _currentCommand = CommandType.Up;
        private CommandType _executingCommand = CommandType.Empty;
        private List<float> _commandTimeList;
        private List<CommandType> _commandList;
        private bool _startExecution;
        private float _executionTime;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _currentCommand = CommandType.Empty;
            _commandList = new List<CommandType>();
            _commandTimeList = new List<float>();
            // Set up teh first command
            _recordCommand(_currentCommand, 0f);
            // Start the delay execution timer
            StartCoroutine(_startExecutionAfterTime(5f));
        }

        // Update is called once per frame
        void Update()
        {
            _checkInput();
            if (_startExecution)
            {
                _fetchCommand();
                _executeCommand();
            }
            ConsoleProDebug.Watch("Player Input Command", _currentCommand.ToString());
            ConsoleProDebug.Watch("Curretn Executing Command", _executingCommand.ToString());
        }

        private void _checkInput()
        {
            if (!Input.anyKey)
            {
                _recordCommand(CommandType.Empty, Time.timeSinceLevelLoad);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _recordCommand(CommandType.Left, Time.timeSinceLevelLoad);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _recordCommand(CommandType.Up, Time.timeSinceLevelLoad);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _recordCommand(CommandType.Right, Time.timeSinceLevelLoad);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _recordCommand(CommandType.Down, Time.timeSinceLevelLoad);
            }
        }

        private void _recordCommand(CommandType cmd, float time = 0f)
        {
            if (cmd == _currentCommand) return;
            _currentCommand = cmd;
            // Record the time and cmd in the list
            _commandList.Add(cmd);
            _commandTimeList.Add(time);
        }

        private void _fetchCommand()
        {
            // Don't fetch anymore command if _commandList is empty
            if (_commandTimeList.Count <= 0) return;

            float nextTimeOnList = _commandTimeList[0];
            // Check if we are ready to fetch next command
            if (Mathf.Abs(_executionTime - nextTimeOnList) <= 0.05f)
            {
                _executingCommand = _commandList[0];
                _commandList.RemoveAt(0);
                _commandTimeList.RemoveAt(0);
            }
            _executionTime += Time.deltaTime;
        }

        // Execute command
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
            // Progress time
        }

        IEnumerator _startExecutionAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            _startExecution = true;
        }
    }
}