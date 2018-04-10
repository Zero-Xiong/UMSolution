﻿using System.Linq;
using UMDataAccess;
using UMDomainEntity;

namespace UMServices
{
    public interface IUserService
    {
        User FindUser(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISecurityService securityService;


        public UserService(IUserRepository _userRepository,
            IUnitOfWork _unitOfWork,
            ISecurityService _securityService)
        {
            userRepository = _userRepository;
            unitOfWork = _unitOfWork;
            securityService = _securityService;
        }

        public User FindUser(string email, string password)
        {
            var passwordHash = securityService.GetSha256Hash(password);

            return userRepository.Find(x => x.Email == email && x.Password == passwordHash)
                .ToList()
                .FirstOrDefault();
        }
    }
}
