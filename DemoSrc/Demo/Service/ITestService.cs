using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Dev.Data.Infras;

namespace Service
{
    public interface ITestService
    {
        IEnumerable<Admin> GetAllAdmin();
        bool Add(Admin model);
        int CheckAdminType();
        Task<List<Admin>> GetAllAdminASync();

        Task<int> CheckAdminTypeASync();
        Task<bool> AddAsync(Admin model);
    }

    class TestService : ITestService
    {
        private readonly Lazy<IRepository<Admin>> _adminRepository;
        private readonly Lazy<IRepository<Admintype>> _admintypeRepository;

        public TestService(Lazy<IRepository<Admin>> adminRepository, Lazy<IRepository<Admintype>> admintypeRepository)
        {
            _adminRepository = adminRepository;
            _admintypeRepository = admintypeRepository;
        }

        public IEnumerable<Admin> GetAllAdmin()
        {
            return _adminRepository.Value.GetAll();
        }

        public bool Add(Admin model)
        {
            this._adminRepository.Value.Add(model);
            return 1 == this._adminRepository.Value.UnitOfWork.SaveChanges();
        }

        public int CheckAdminType()
        {
            var one = _admintypeRepository.Value.FindOne(x => true);

            if (one == null)
            {
                one = new Admintype
                {
                    Purviews = "",
                    System = true,
                    Typename = "typename"
                };
                this._admintypeRepository.Value.Add(one);
                this._admintypeRepository.Value.UnitOfWork.SaveChanges();
            }

            return one.Typeid;
        }

        public Task<List<Admin>> GetAllAdminASync()
        {
            return _adminRepository.Value.GetAllAsync();
        }

        public async Task<int> CheckAdminTypeASync()
        {
            var one = await _admintypeRepository.Value.FindOneAsync(x => true);

            if (one == null)
            {
                one = new Admintype
                {
                    Purviews = "",
                    System = true,
                    Typename = "typename"
                };
                this._admintypeRepository.Value.Add(one);
                this._admintypeRepository.Value.UnitOfWork.SaveChanges();
            }

            return one.Typeid;
        }

        public async Task<bool> AddAsync(Admin model)
        {
            this._adminRepository.Value.Add(model);
            return 1 == await this._adminRepository.Value.UnitOfWork.SaveChangesAsync();
        }
    }
}
