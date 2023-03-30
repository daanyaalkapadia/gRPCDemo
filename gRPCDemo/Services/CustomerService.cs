using Grpc.Core;
using gRPCDemo;

namespace gRPCServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        public override Task<CustomerDto> GetCustomerInfo(CustomerLookUp request, ServerCallContext context)
        {
            return Task.FromResult(
                new CustomerDto() { Id = request.Id, FirstName = "DK", LastName = "DK" }
                );
        }
        public async override Task GetAllCustomerInfo(CustomerLookUp request, IServerStreamWriter<CustomerDto> responseStream, ServerCallContext context)
        {
            List<CustomerDto> customerDtos = new List<CustomerDto>()
            {
                new CustomerDto() { Id = request.Id, FirstName = "DK", LastName = "DK" },
                new CustomerDto() { Id = request.Id, FirstName = "VK", LastName = "VK" }
            };
            foreach(var cust in customerDtos)
            {
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
