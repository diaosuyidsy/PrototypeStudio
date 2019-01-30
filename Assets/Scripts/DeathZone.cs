﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpringTime
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.GM.OnCalculateScore();
            }
        }
    }
}

