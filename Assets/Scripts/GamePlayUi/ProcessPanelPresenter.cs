﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class ProcessPanelPresenter : MonoBehaviour, IDisposable
    {
        [SerializeField] Button closeButton;
        [SerializeField] Text text;

        static int count = 0;
        public event Action closed;
        bool closedFlag = false;

        public int ProcessId { get; private set; }

        void Awake()
        {
            count++;
            ProcessId = count;
            text.text = "プロセス:" + count;
            closeButton.onClick.AddListener(() => closed?.Invoke());
        }

        void Update()
        {
            if (closedFlag)
            {
                closed = null;
                Destroy(gameObject);
            }
        }


        public void Dispose()
        {
            closedFlag = true;
        }
    }
}