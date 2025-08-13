using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatP2P;

public class Peer
{
    private readonly TcpListener _tcpListener;
    private TcpClient? _tcpClient;
    private const int Port = 8080;
    public Peer() => _tcpListener = new TcpListener(IPAddress.Any, Port);

    public async Task ConnectToPeer(string ipAddress, string port)
    {
        try
        {
            _tcpClient = new TcpClient(ipAddress, Convert.ToInt32(port));
            Console.WriteLine("Connection stabllished! :D");

            var receiveTask = ReceiveMessage();
            await SendMessage(); // await sirve para enfocar el hilo principal en esta tarea
            await receiveTask; 
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to peer: " + ex.Message);
        }
    }


    public async Task StartListening()
    {
        try
        {
            // intento ejecutar logica
            _tcpListener.Start(); // Inicio del Listener abriendo el puerto 8080
            Console.WriteLine("Lisening for incoming connections...");
            _tcpClient = await _tcpListener.AcceptTcpClientAsync(); // El codigo se queda en loop escuchando
            Console.WriteLine("Connection established! :D");

            var receiveTask = ReceiveMessage();
            await SendMessage(); // await sirve para enfocar el hilo principal en esta tarea
            await receiveTask;
        }
        catch (Exception ex)
        {
            //Log del error
            Console.WriteLine("Connection closed :()" + ex.Message);
        }
    }

    public async Task ReceiveMessage() // Solo va a recibir mensajes
    {
        try
        {
            var stream = _tcpClient!.GetStream(); // Solicitar un hilo en el que viaja binario osea el mensaje
            var reader = new StreamReader(stream, Encoding.UTF8); // Meter los bites en un buffer Contenedor donde guardamos informacion guardandolo en formato UTF8 que es loque convierte en string
            var message = await reader.ReadLineAsync(); // Al detectar el final del mensaje lo guardamos en la variable ya como string
            Console.WriteLine($"Peer message: {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error receiving message " + ex.Message);
        }
        finally
        {
            Close();
        }
    }

    public async Task SendMessage()
    {
        try
        {
            var stream = _tcpClient!.GetStream();
            var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            var message = "Hola :D este es mi primer mensaje";
            await writer.WriteLineAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending message: " + ex.Message);
        }
        finally
        {
            Close();
        }
    }

    private void Close()
    {
        _tcpClient?.Close();
        _tcpListener.Stop(); // Cerrando el puerto 8080 ya que se acabo
    }
}