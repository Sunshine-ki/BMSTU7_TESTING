using System;

namespace Testing.Builders
{    
	public class UserBLBuilder
    {
        private bl.User _user;

        public UserBLBuilder()
        {
            _user = new bl.User();
        }

        public UserBLBuilder WithName(string name)
        {
            _user.Name = name;
            return this;
        }

        public UserBLBuilder WithEmail(string email)
        {
            _user.Email = email;
            return this;
        }

        public UserBLBuilder WithLogin(string login)
        {
            _user.Login = login;
            return this;
        }

        
        public UserBLBuilder WithPassword(string password)
        {
            _user.Password = password;
            return this;
        }

        public bl.User Build()
        {
            return _user;
        }
    }

}