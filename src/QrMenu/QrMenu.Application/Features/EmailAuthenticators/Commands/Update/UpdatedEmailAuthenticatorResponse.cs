using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.Application.Features.EmailAuthenticators.Commands.Update;

public class UpdatedEmailAuthenticatorResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ActivationKey { get; set; }
    public Boolean IsVerified { get; set; }
}
