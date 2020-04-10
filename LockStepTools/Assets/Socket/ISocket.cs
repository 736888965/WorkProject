using System;

public interface ISocket
{
    /// <summary>
    /// 获取是否已连接。
    /// </summary>
    bool IsConnected { get; }
    /// <summary>
    /// 发送数据。
    /// </summary>
    /// <param name="data">要发送的数据。</param>
    void Send(byte[] data);
    /// <summary>
    /// 异步发送数据。
    /// </summary>
    /// <param name="data">要发送的数据。</param>
    void SendAsync(byte[] data);
    /// <summary>
    /// 断开连接。
    /// </summary>
    void Disconnect();
    /// <summary>
    /// 异步断开连接。
    /// </summary>
    void DisconnectAsync();
    /// <summary>
    /// 断开完成时引发事件。
    /// </summary>
    event EventHandler<SocketEventArgs> DisconnectCompleted;
    /// <summary>
    /// 接收完成时引发事件。
    /// </summary>
    event EventHandler<SocketEventArgs> ReceiveCompleted;
    /// <summary>
    /// 发送完成时引发事件。
    /// </summary>
    event EventHandler<SocketEventArgs> SendCompleted;
}