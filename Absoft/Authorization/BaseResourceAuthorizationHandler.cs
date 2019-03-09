using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Absoft.Authorization
{
    public class BaseResourceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, string>
    {
        private readonly IRoleRepository _roleRepository;

        public BaseResourceAuthorizationHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, string resource)
        {
            var rolesClaim = context.User.FindAll(ClaimTypes.Role);
            List<string> roles = new List<string>();
            foreach (var item in rolesClaim)
            {
                roles.Add(item.Value);
            }

            if (roles.Count > 0)
            {
                var hasPermission = await _roleRepository.CheckPermissionAsync(resource, requirement.Name, roles.ToArray());
                if (hasPermission || roles.Contains("Admin"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
        }
    }
}
