using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.BussinessLogic.UnitOfWork.Password;

namespace User.API.DependencyInjection
{
    public static class BussinessLogicInjection
    {
        public static void RegisterBussinessLogicInjection(this IServiceCollection services)
        {
            services.AddTransient<IPasswordUnitOfWork, PasswordUnitOfWork>();
            services.AddTransient<IPasswordUnitOfWork, PasswordUnitOfWork>();
        }
    }
}
