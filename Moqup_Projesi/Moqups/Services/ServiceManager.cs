﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAuthService _authService;

        public ServiceManager(IAuthService authService)
        {
            _authService = authService;
        }

        public IAuthService AuthService => _authService;
    }
}
