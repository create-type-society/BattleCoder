using System;
using System.IO;
using UnityEngine;

public class DeviceController : IUserInput
{
    public event EventHandler<EventArgs> MeleeAttackEvent;
    public event EventHandler<EventArgs> ShootingAttackEvent;

    private DeviceClientListener _listener = new DeviceClientListener();

    public DeviceController()
    {
        _listener.AcceptClient();
        Application.quitting += () => { _listener.Close(); };
    }

    public void Update()
    {
        if (_listener.IsAccept)
        {
            ListenPacket();
        }
    }

    private async void ListenPacket()
    {
        var packet = await _listener.ReadPacket();
        DecodePacket(packet);
    }

    private void DecodePacket(PacketData packet)
    {
        var stream = new MemoryStream(packet.Data);
        var br = new BinaryReader(stream);
        if (packet.Type == PacketType.InputDeviceData)
        {
            DecodeInputDeviceData(br);
        }
        else if (packet.Type == PacketType.InputDeviceButtonData)
        {
            DecodeInputDeviceButtonData(br);
        }

        br.Close();
        stream.Close();
    }

    private void DecodeInputDeviceData(BinaryReader br)
    {
        if (br.BaseStream.Length <= 4)
            return;

        var deviceVal = br.ReadSingle();
        if (deviceVal >= 3)
        {
            OnMeleeAttackEvent(this, EventArgs.Empty);
        }
    }

    private void DecodeInputDeviceButtonData(BinaryReader br)
    {
        OnShootingAttack(this, EventArgs.Empty);
    }

    private void OnMeleeAttackEvent(object sender, EventArgs args)
    {
        MeleeAttackEvent?.Invoke(sender, args);
    }

    private void OnShootingAttack(object sender, EventArgs args)
    {
        ShootingAttackEvent?.Invoke(sender, args);
    }
}