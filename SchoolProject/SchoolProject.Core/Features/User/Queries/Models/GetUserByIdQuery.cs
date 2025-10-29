
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery:IRequest<Response<GetUserByIdResult>>
    {
        public string Id { get; set; }
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
