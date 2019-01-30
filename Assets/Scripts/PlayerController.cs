using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpringTime
{
    public class PlayerController : MonoBehaviour
    {
        private enum CommandType
        {
            Empty,
            Up,
            Down,
            Left,
            Right
        }
        private CommandType _currentCommand;
        private List<float> _commandTimeList;
        private List<CommandType> _commandList;

        // Start is called before the first frame update
        void Start()
        {
            _currentCommand = CommandType.Empty;
            _commandList = new List<CommandType>();
            _commandTimeList = new List<float>();
            // Set up teh first command
            _recordCommand(_currentCommand, 0f);
        }

        // Update is called once per frame
        void Update()
        {
            _checkInput();
            ConsoleProDebug.Watch("Player Input Command", _currentCommand.ToString());
        }

        private void _checkInput()
        {
            if (!Input.anyKey)
            {
                _recordCommand(CommandType.Empty, Time.time);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _recordCommand(CommandType.Left, Time.time);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _recordCommand(CommandType.Up, Time.time);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _recordCommand(CommandType.Right, Time.time);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _recordCommand(CommandType.Down, Time.time);
            }
        }

        private void _recordCommand(CommandType cmd, float time = 0f)
        {
            _currentCommand = cmd;
        }
    }
}


