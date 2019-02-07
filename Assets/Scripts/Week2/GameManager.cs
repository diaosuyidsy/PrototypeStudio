﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Week2
{
    [RequireComponent(typeof(AudioSource))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager GM;
        public AudioClip[] ChipEatingClips;
        public TextAsset TextScript;
        public Text ScripterText;

        [HideInInspector]
        public State GameState;

        private int _clipPointer;
        private int _scriptPointer;
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
                if (st.Length == 0 || st[0] != ';')
                {
                    _progressAudio();
                }
                else
                {
                    st = st.Substring(1);
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

