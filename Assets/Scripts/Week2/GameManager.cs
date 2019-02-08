using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Week2
{
    [RequireComponent(typeof(AudioSource))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager GM;
        public AudioClip[] ChipEatingClips;
        public TextAsset TextScript;
        public Text ScripterText;
        public RectTransform Achievement;

        [HideInInspector]
        public State GameState;

        private int _clipPointer;
        private int _scriptPointer;
        private int _chipCounter;
        private int _cc;
        private AudioSource _as;
        private string[] _scriptTexts;

        private void Awake()
        {
            GM = this;
            GameState = State.Start;
            _as = GetComponent<AudioSource>();
            _initializeTextScript();

        }

        private void _initializeTextScript()
        {
            _scriptTexts = TextScript.text.Split('\n');
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        void Update()
        {
            _checkInput();
        }

        /// <summary>
        /// Checks the input during Game Start Phase
        /// </summary>
        private void _checkInput()
        {
            if (GameState != State.Start) return;
            if (Input.GetMouseButtonDown(0) && !_as.isPlaying)
            {
                _progressText();
            }
        }

        private void _progressAudio()
        {
            if (!_as.isPlaying)
            {
                _as.clip = ChipEatingClips[_clipPointer];
                _as.Play();
                _clipPointer = (_clipPointer + 1) % ChipEatingClips.Length;
            }
        }

        /// <summary>
        /// Progresses the text by 1 at a time
        /// </summary>
        private void _progressText()
        {
            if (_scriptPointer < _scriptTexts.Length)
            {
                // If script text is "", then skip text, just progress audio
                // If script text starts with ";", skip audio
                // Otherwise progress both
                string st = _scriptTexts[_scriptPointer++];
                if (st.Length != 0 && st[0] == ';')
                {
                    // Skip Audio
                    st = st.Substring(1);
                }
                else if (st.Length != 0 && st[0] == '/')
                {
                    // cut string, add counter to it
                    st = st.Substring(1);
                    if (_chipCounter != 0 && _chipCounter % ChipEatingClips.Length == 0) st = string.Format(st, _chipCounter++ - ++_cc);
                    else st = string.Format(st, _chipCounter++ - _cc);
                    _progressAudio();
                }
                else if (st == "[Achievement]")
                {
                    st = "";
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(Achievement.DOAnchorPosY(586f, 0.25f)).AppendInterval(3f).Append(Achievement.DOAnchorPosY(900f, 0.25f));
                    _progressAudio();
                }
                else
                {
                    _progressAudio();
                }
                ScripterText.text = st;
            }
            else
            {
                ScripterText.text = "Game End";
                _progressAudio();
            }
        }
    }

    public enum State
    {
        PreStart,
        Start,
        End
    }
}

