using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpringTime
{
    public class PlayerController : MonoBehaviour
    {
        #region Normal Variables
        public float WindForce = 100f;
        public float MaxVelocity = 10f;

        private Rigidbody2D _rb;
        #endregion

        #region Command Related Variables
        public float CommandDelayTime = 5f;
        public GameObject PlayerShadow;

        // Set the command current command to be something not empty
        private CommandType _currentCommand = CommandType.Up;
        private CommandType _executingCommand = CommandType.Empty;
        private List<float> _commandTimeList;
        private List<CommandType> _commandList;
        private bool _startExecution;
        private float _executionTime;
        private GameObject _curCommandVisual = null;
        private float _commandVisualTimer;
        private float _recordTime;
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

        }

        // Update is called once per frame
        void Update()
        {
            if (!_startExecution)
            {
                _checkInput();
                if (GameManager.GM.State == GameManager.GameState.Record)
                    _generateCurCommandVisual();
            }
            if (_startExecution)
            {
                _fetchCommand();
                _executeCommand();
                _checkEndCritiria();
            }
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, MaxVelocity);
        }

        private void _checkInput()
        {
            if (Input.anyKey && GameManager.GM.State == GameManager.GameState.Prepare)
            {
                // Start the delay execution timer
                StartCoroutine(_startExecutionAfterTime(CommandDelayTime));
                GameManager.GM.State = GameManager.GameState.Record;
                GameManager.GM.HintCanvas.SetActive(false);
            }
            if (GameManager.GM.State == GameManager.GameState.Record) _recordTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _recordCommand(CommandType.Left, _recordTime);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _recordCommand(CommandType.Up, _recordTime);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _recordCommand(CommandType.Right, _recordTime);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _recordCommand(CommandType.Down, _recordTime);
            }
        }

        private void _recordCommand(CommandType cmd, float time = 0f)
        {
            if (cmd == _currentCommand) return;

            // Record past command into Command Recorder
            if (_currentCommand != CommandType.Empty)
            {
                CommandRecorder.CR.AddCommand(_currentCommand, time - _commandTimeList[_commandTimeList.Count - 1]);
            }
            _currentCommand = cmd;
            // Record the time and cmd in the list
            _commandList.Add(cmd);
            _commandTimeList.Add(time);

            // Generate Current Command Record Visual
            if (_currentCommand != CommandType.Empty)
            {
                _commandVisualTimer = 0f;
                _curCommandVisual = Instantiate(CommandRecorder.CR.CommandVisualPrefab);
                _curCommandVisual.transform.SetParent(CommandRecorder.CR.CurCommandContainer.transform, false);
                _curCommandVisual.GetComponent<VisualCommandSetup>().Setup(_currentCommand, _commandVisualTimer);
            }
        }

        private void _generateCurCommandVisual()
        {
            if (_curCommandVisual != null)
            {
                _commandVisualTimer += Time.deltaTime;
                _curCommandVisual.GetComponent<VisualCommandSetup>().Setup(_commandVisualTimer);
            }
        }

        private void _fetchCommand()
        {
            _executionTime += Time.deltaTime;
            // Don't fetch anymore command if _commandList is empty
            if (_commandTimeList.Count <= 0) return;

            float nextTimeOnList = _commandTimeList[0];
            // Check if we are ready to fetch next command
            if (Mathf.Abs(_executionTime - nextTimeOnList) <= 0.05f)
            {
                _executingCommand = _commandList[0];
                Debug.LogFormat("Executing Command {0} at Time {1}", _executingCommand.ToString(), _executionTime);
                _commandList.RemoveAt(0);
                _commandTimeList.RemoveAt(0);
            }
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

        private void _checkEndCritiria()
        {
            if (_rb.velocity.magnitude <= 0.1f && _commandTimeList.Count <= 0)
            {
                GameManager.GM.OnCalculateScore();
            }
        }

        IEnumerator _startExecutionAfterTime(float time)
        {
            // Generate a player shadow if this is not the first time
            if (CommandRecorder.CR.GetPrevCmdList().Count > 0)
                Instantiate(PlayerShadow, transform.position, Quaternion.identity);

            // End Generation
            Text CountDownText = GameManager.GM.CountdownText;
            float elapsedTime = time;
            while (elapsedTime > 0f)
            {
                CountDownText.text = elapsedTime.ToString("F1");
                elapsedTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            CountDownText.text = "0.0";
            // Record past command into Command Recorder
            if (_currentCommand != CommandType.Empty)
            {
                CommandRecorder.CR.AddCommand(_currentCommand, _recordTime - _commandTimeList[_commandTimeList.Count - 1]);
            }
            _startExecution = true;
        }
    }
    public enum CommandType
    {
        Empty,
        Up,
        Down,
        Left,
        Right
    }
}

