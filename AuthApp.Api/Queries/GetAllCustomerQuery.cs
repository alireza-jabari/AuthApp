using MediatR;

namespace AuthApp.Queries;

//1.create request for mediatR
// specify type of response in IRequest
public class GetAllCustomerNameQuery:IRequest<List<string>>
{

}