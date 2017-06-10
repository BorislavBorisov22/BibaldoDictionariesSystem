using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Users;
using System.Linq;

namespace DictionariesSystem.Framework.Core.Providers
{
    public class UserProvider : IUserProvider
    {
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
            var targetUser = this.usersRepository
                .All(x => x.Username == username && x.Passhash == password)
                .FirstOrDefault();

            Guard.WhenArgument(targetUser, "Provider username or password is not valid").IsNull().Throw();
            this.loggedUser = targetUser;
        }

        public void Logout()
        {
            this.loggedUser = null;
        }

        public void Register(string username, string password)
        {
            var existingUser = this.usersRepository.All(x => x.Username == username).FirstOrDefault();

            Guard.WhenArgument(existingUser, "Such user already exists").IsNotNull().Throw();

            var newUser = this.userFactory.GetUser(username, password);
            this.usersRepository.Add(newUser);

            this.unitOfWork.SaveChanges();
        }
    }
}