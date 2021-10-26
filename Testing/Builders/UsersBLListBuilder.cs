using System;
using System.Collections.Generic;


namespace Testing.Builders
{
	public class UsersBLListBuilder
    {
        private List<bl.User> _users;

        public UsersBLListBuilder(int count)
        {
            _users = new List<bl.User>(count);
        }
        public List<bl.User> Build()
        {
            return _users;
        }
    }    

}