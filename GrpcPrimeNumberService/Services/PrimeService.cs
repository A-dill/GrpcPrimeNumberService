using Grpc.Core;
using System;
using System.Collections.Concurrent;

namespace GrpcPrimeNumberService.Services
{
    public class PrimeService : PrimeCheck.PrimeCheckBase
    {
        private readonly ConcurrentDictionary<long, int> highestPrimeNumbers = new ConcurrentDictionary<long, int>();
        private int totalMessagesReceived = 0;
        public PrimeService(ILogger<PrimeService> logger)
        {
            StartDisplayTopPrimes();
        }

        public override Task<PrimeReply> CheckPrime(PrimeRequest request, ServerCallContext context)
        {
            totalMessagesReceived++;
            return Task.Run(() =>
            {
                bool isPrime = IsPrime(request.Number);

                if (isPrime)
                {
                    highestPrimeNumbers.AddOrUpdate(request.Number, 1, (_, count) => count + 1);
                }
                return new PrimeReply {
                    Id = request.Id,
                    Isprime = isPrime,
                    Timestamp = request.Timestamp,
                    Number = request.Number
                };
            });
        }
        public void StartDisplayTopPrimes()
        {
            var task = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    TopRequestedPrimes();

                }
            });
        }
        public void TopRequestedPrimes()
        {
            Console.WriteLine($"Total No. of Requests Received:{totalMessagesReceived}");
            Console.WriteLine("Top 10 Highest Requested/Validated Prime Numbers:");
            var topTenPrimeNumbers = highestPrimeNumbers.OrderByDescending(kv => kv.Key).Take(10);
            foreach (var item in topTenPrimeNumbers)
            {
                Console.WriteLine($"{item.Key}");
            }
        }
        public bool IsPrime(long number)
        {
            if(number <=1)
                return false;
            for (long i = 2; i <= Math.Sqrt(number); i++)
            {
                if(number % i == 0)
                    return false;
            }
            return true;
        }
    }
}

