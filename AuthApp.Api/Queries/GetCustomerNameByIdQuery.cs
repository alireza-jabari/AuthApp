using MediatR;

namespace AuthApp.Queries;


public class GetCustomerNameByIdQuery:IRequest<string>
{
    public int CustomerId { get; }
    public GetCustomerNameByIdQuery(int id)
    {
        CustomerId=id;
    }

}