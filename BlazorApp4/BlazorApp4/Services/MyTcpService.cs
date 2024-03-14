using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using BlazorApp4.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace BlazorApp4.Services
{
    

    public class MyTcpBackgroundService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private readonly TcpListener _tcpListener;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public MyTcpBackgroundService(IServiceProvider services)
        {
            _services = services;
            _tcpListener = new TcpListener(IPAddress.Any, 12345); 
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _tcpListener.Start();
            Task.Run(() => AcceptConnectionsAsync(_cancellationTokenSource.Token));

            return Task.CompletedTask;
        }

        private async Task AcceptConnectionsAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                    _ = HandleClientAsync(client, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AcceptConnectionsAsync: {ex.Message}");
            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024*100];
                int bytesRead;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    string data1 = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received data: {data1}");
                    var user_file =data1.Split(",");
                    var usr = user_file[0];
                    using (var scope = _services.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var user = context.Users.First(x => x.UserName.Equals(usr));
                        string fileName = user_file[1] + ".pdf";

                        Aes aes = Aes.Create();
                        aes.Key = user.aesKey;
                        aes.IV = user.iv;
                        
                        using (MemoryStream encryptedMemoryStream = new MemoryStream())
                        {
                            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(encryptedMemoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    byte[] data = File.ReadAllBytes(fileName);
                                    cryptoStream.Write(data, 0, data.Length);
                                    cryptoStream.FlushFinalBlock();
                                    var response = encryptedMemoryStream.ToArray();
                                    stream.Write(response, 0, response.Length);
                                }
                            }
                        }

                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in HandleClientAsync: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _tcpListener.Stop();
            _cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _tcpListener?.Stop();
            _cancellationTokenSource?.Dispose();
        }
    }

}
