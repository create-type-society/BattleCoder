using System;
using System.IO;

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

        br.Close();
        stream.Close();
    }

    private void DecodeInputDeviceData(BinaryReader br)
    {
        var deviceVal = br.ReadSingle();
        if (deviceVal >= 3)
        {
            OnMeleeAttackEvent(this, EventArgs.Empty);
        }
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