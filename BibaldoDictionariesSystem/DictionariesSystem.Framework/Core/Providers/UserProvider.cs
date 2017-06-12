using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Exceptions;
using DictionariesSystem.Models.Users;
using System.Linq;

namespace DictionariesSystem.Framework.Core.Providers
{
    public class UserProvider : IUserProvider
    {
        private const string InvalidLoginMessage = "Invalid username or password!";
        private const string InvalidRegisterMessage = "Such user already exists!";
        private const string InvalidLogoutMessage = "You must login first!";
        private const string AlreadyLoggedInMessage = "You must logout first!";

        private readonly IRepository<User> usersRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserFactory userFactory;

        private User loggedUser;

        public UserProvider(IRepository<User> usersRepository, IUnitOfWork unitOfWork, IUserFactory userFactory)
        {
            Guard.WhenArgument(usersRepository, "usersRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(userFactory, "userFactory").IsNull().Throw();

            this.usersRepository = usersRepository;
            this.unitOfWork = unitOfWork;
            this.userFactory = userFactory;
        }

        public User LoggedUser
        {
            get
            {
                return this.loggedUser;
            }

            protected set
            {
                Guard.WhenArgument(value, "LoggedUser").IsNull().Throw();

                this.loggedUser = value;
            }
        }
      

        public bool IsLogged
        {
            get
            {
                return this.loggedUser != null;
            }
        }

        public void Login(string username, string password)
        {
            if (this.IsLogged)
            {
                throw new UserAuthenticationException(AlreadyLoggedInMessage);
            }

            var targetUser = this.usersRepository
                .All(x => x.Username == username && x.Passhash == password)
                .FirstOrDefault();

            if (targetUser == null)
            {
                throw new UserAuthenticationException(InvalidLoginMessage);
            }

            this.loggedUser = targetUser;
        }

        public void Logout()
        {
            if (this.loggedUser == null)
            {
                throw new UserAuthenticationException(InvalidLogoutMessage);
            }

            this.loggedUser = null;
        }

        public void Register(string username, string password)
        {
            if (this.IsLogged)
            {
                throw new UserAuthenticationException(AlreadyLoggedInMessage);
            }

            var existingUser = this.usersRepository.All(x => x.Username == username).FirstOrDefault();
            if (existingUser != null)
            {
                throw new UserAuthenticationException(InvalidRegisterMessage);
            }

            var newUser = this.userFactory.GetUser(username, password);
            this.usersRepository.Add(newUser);

            this.unitOfWork.SaveChanges();
        }
    }
}