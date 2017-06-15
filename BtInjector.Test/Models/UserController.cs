﻿namespace BtInjector.Test.Models
{
    public class UserController
    {
        private IRepository repository;
        private IAuthenticationService authService;

        //[Inject]
        public UserController(IRepository repository, IAuthenticationService authService)
        {
            this.repository = repository;
            this.authService = authService;
        }
    }
}
