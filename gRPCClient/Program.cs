using Grpc.Core;
using Grpc.Net.Client;
using gRPCDemo;
using System.Diagnostics;

namespace gRPCClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch sp = new Stopwatch();
            var channel = GrpcChannel.ForAddress("https://localhost:7206/");
            var client = new Greeter.GreeterClient(channel);
            sp.Start();
            var reply = await client.SayHelloAsync(new HelloRequest() { Name = "Daanyaal" });
            sp.Stop();
            Console.WriteLine("Message received using gRPC :" + reply.Message);
            Console.WriteLine("Elapse Milliseconds :" + sp.ElapsedMilliseconds);

            var customerClient = new Customer.CustomerClient(channel);
            var customer = await customerClient.GetCustomerInfoAsync(new CustomerLookUp() { Id = 1 });
            Console.WriteLine("Customer Name " + customer.FirstName);

            using(var cust = customerClient.GetAllCustomerInfo(new CustomerLookUp()))
            {
                while (await cust.ResponseStream.MoveNext())
                {
                    var curr = cust.ResponseStream.Current;
                    Console.WriteLine("Customer Name " + curr.FirstName);
                }
            }
            Console.ReadLine();
        }
    }
}