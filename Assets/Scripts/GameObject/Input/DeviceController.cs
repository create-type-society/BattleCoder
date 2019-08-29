using System;
using UnityEngine;

namespace BattleCoder.GameObject.Input
{
    public class DeviceController : IUserInput
    {
        public event EventHandler<EventArgs> MeleeAttackEvent;
        public event EventHandler<EventArgs> ShootingAttackEvent;

        private DeviceClientListener _listener = new DeviceClientListener();

        public DeviceController()
        {
            _listener.AcceptClient();
        }

        ~DeviceController()
        {
            _listener.Close();
        }

        public void Update()
        {
            if (_listener.IsAccept)
            {
                Debug.Log("接続完了");
            }
        }
    }
}