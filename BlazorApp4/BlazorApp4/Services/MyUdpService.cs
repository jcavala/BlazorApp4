namespace BlazorApp4.Services
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    public class UdpBackgroundService : BackgroundService
    {
        private readonly UdpClient _udpClient;

        public UdpBackgroundService()
        {
            
            _udpClient = new UdpClient(12346); 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var udpResult = await _udpClient.ReceiveAsync();
                    string signature = Encoding.UTF8.GetString(udpResult.Buffer) + "_signed.bin";
                    var signedBytes =  File.ReadAllBytes(signature);
                    await _udpClient.SendAsync(signedBytes, signedBytes.Length, udpResult.RemoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UdpBackgroundService: {ex.Message}");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _udpClient.Close();
            await base.StopAsync(cancellationToken);
        }
    }

}
