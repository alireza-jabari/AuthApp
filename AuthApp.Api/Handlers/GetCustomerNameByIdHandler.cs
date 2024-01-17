using AuthApp.Queries;
using MediatR;

namespace AuthApp.Handlers;


//type of request,type of return response
public class GetCustomerNameByIdHandler : IRequestHandler<GetCustomerNameByIdQuery, string>
{
    public async Task<string> Handle(GetCustomerNameByIdQuery request, CancellationToken cancellationToken)
    {
        if(request.CustomerId == 123){
            return "akabar";
        }else{
            return "hello";
        }
    }
}