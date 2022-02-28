using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class Serial : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived = delegate { };

    public string portName = "COM4";        //各自のマイコンのCOMポート
    public int baudRate = 9600;
    public GameObject vas;
    public GameObject vasslider;
    public float VAS;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

  private void Awake()
    {
        Open();
    }
    

    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
            VAS = float.Parse(message_) / 1023 * 100;
            vas.GetComponent<Text>().text =VAS.ToString("F0");
            vasslider.GetComponent<Slider>().value = VAS;
            //Debug.Log(message_);        //受信したデータの確認表示用
        }
    }

    void OnDestroy()
    {
        Close();
    }

    public void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.ReadTimeout = 20;
        serialPort_.Open();
        serialPort_.NewLine = "\n";

        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void Close()
    {
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            try
            {
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
