using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [SerializeField] private InputField inputFieldIP;
    [SerializeField] protected SelectNameSystem selectNameSystem;
    [SerializeField] private InputPacketHandler inputPacketHandler;

    private TcpClient client;
    private NetworkStream stream;

    private int port = 7000;
    private bool connected = false;
    private Thread receiveThread;

    public void Connect()
    {
        try
        {
            client = new TcpClient(inputFieldIP.text, port);
            stream = client.GetStream();
            connected = true;
            PostData($"Hi, my name is {selectNameSystem.GetName()} and I've successfully connected!");

            // Запуск потока для получения данных
            receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public void Disconnect()
    {
        if (connected)
        {
            try
            {
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
            finally
            {
                connected = false;
            }

            if (receiveThread != null && receiveThread.IsAlive)
            {
                receiveThread.Abort();
            }
        }
    }

    public void PostData(Package package)
    {
        string json = JsonDataSerializer.SerializeToJsonString(package);
        PostData(json);
    }

    public void PostData(string message)
    {
        if (connected)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(message);

                if (stream.CanWrite)
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
                else
                {
                    Debug.LogError("Stream is not writable.");
                    Disconnect();
                }
            }
            catch (IOException ioEx)
            {
                Debug.LogError(ioEx);
                Disconnect();
            }
            catch (SocketException sockEx)
            {
                Debug.LogError(sockEx);
                Disconnect();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                Disconnect();
            }
        }
    }

    private void ReceiveData()
    {
        try
        {
            byte[] bytes = new byte[512];
            while (connected)
            {
                int bytesRead = stream.Read(bytes, 0, bytes.Length);
                if (bytesRead > 0)
                {
                    string message = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                    ProcessReceivedData(message);
                }
            }
        }
        catch (IOException ioEx)
        {
            Debug.LogError(ioEx);
        }
        catch (SocketException sockEx)
        {
            Debug.LogError(sockEx);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private void ProcessReceivedData(string message)
    {
        if (IsJson(message))
        {
            Package package = JsonDataSerializer.DeserializeJson<Package>(message);
            inputPacketHandler.ProcessPackage(package);
            MyConsole.Instance.DebugLog($"Received package: {package.NamePackage}");
        }
        else
        {
            MyConsole.Instance.DebugLog(message);
        }
    }

    private bool IsJson(string input)
    {
        input = input.Trim();
        return (input.StartsWith("{") && input.EndsWith("}")) || (input.StartsWith("[") && input.EndsWith("]"));
    }

    public bool IsConnected()
    {
        return connected;
    }
}
