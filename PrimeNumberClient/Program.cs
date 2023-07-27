using Grpc.Net.Client;
using System.Diagnostics;
using GrpcPrimeNumberService;
using Grpc.Core;

var handler = new HttpClientHandler();

handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

var httpClient = new HttpClient(handler);

var channel = GrpcChannel.ForAddress("http://localhost:4026", new GrpcChannelOptions { HttpClient = httpClient });
var random = new Random();
var stopwatch = new Stopwatch();
var client = new PrimeCheck.PrimeCheckClient(channel);
Start();

#region
async Task Start()
{
    while (true)
    {
        int totalRequests = 0;
        stopwatch.Start();
        Console.WriteLine($"Waiting Response for 10000 Requests....");

        var tasks = new List<Task<PrimeReply>>();
        for (int i = 0; i < 10000; i++)
        {
            var request = new PrimeRequest
            {
                Id = i + 1,
                Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Number = random.Next(1, 1000)
            };
            totalRequests = totalRequests + 1; ;
            tasks.Add(SendRequestAsync(client, request));
        }
        await Task.WhenAll(tasks);

        stopwatch.Stop();
        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
        stopwatch.Reset();
        int successfulRequests = tasks.Count(task => task.Status == TaskStatus.RanToCompletion);
        foreach (var task in tasks)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var response = task.Result;
                Console.WriteLine($"Response for Request {response.Id}: Number:{response.Number}: IsPrime: {response.Isprime}: Response time:{DateTimeOffset.Now.ToUnixTimeSeconds() - response.Timestamp}ms");
            }
            else if (task.IsFaulted && task.Exception != null)
            {
                var request = (task.AsyncState as PrimeRequest);
                Console.WriteLine($"Request {request.Id}: An error occurred: {task.Exception.InnerException.Message}");
            }
        }
        Console.WriteLine($"Elapsed Time: {elapsedSeconds:F2} seconds");
        Console.WriteLine($"Total Requests: {totalRequests} Successful Requests: {successfulRequests}");
        await Task.Delay(1000);
    }

}
#endregion


static async Task<PrimeReply> SendRequestAsync(PrimeCheck.PrimeCheckClient client, PrimeRequest request)
{
    try
    {
        var response = await client.CheckPrimeAsync(request);
        return response;
    }
    catch (RpcException ex)
    {
        Console.WriteLine($"Request {request.Id}: An error occurred: {ex.Status.Detail}");
        return null;
    }
}
Console.ReadKey();