using AuthApp.Queries;
using MediatR;

namespace AuthApp.Handlers;


//type of request,type of return response
public class GetAllCustomerNameHandler : IRequestHandler<GetAllCustomerNameQuery, List<string>>
{
    public async Task<List<string>> Handle(GetAllCustomerNameQuery request, CancellationToken cancellationToken)
    {
        List<string> customers=new List<string>{
            "alireza","jabari"
        };
        return customers;
    }
}