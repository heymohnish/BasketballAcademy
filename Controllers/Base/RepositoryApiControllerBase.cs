using BasketballAcademy.Repository.Interface;

namespace BasketballAcademy.Controllers.Base
{
    public class RepositoryApiControllerBase<T> : ApiControllerBase where T : IRepository
    {
        protected readonly T repository;

        public RepositoryApiControllerBase(T repository)
        {
            this.repository = repository;
        }
    }
}
