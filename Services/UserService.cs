using System;
using System.Linq;
using SocialFreeks.Entities;
using SocialFreeks.Repository;

namespace Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Find(Guid id)
        {
            return _userRepository.Get(id);
        }

        public User FindByUserName(string username)
        {
            return (from x in _userRepository.All()
                                   where x.UserName == username
                                   select x).FirstOrDefault();
        }

        public void Save(User user)
        {
            _userRepository.Save(user);
        }
    }
}
