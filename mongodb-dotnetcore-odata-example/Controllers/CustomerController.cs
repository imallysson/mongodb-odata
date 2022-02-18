using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using mongodb_dotnetcore_odata_example.Models;
using mongodb_dotnetcore_odata_example.Repositories;

namespace mongodb_dotnetcore_odata_example.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Customer> Get()
        {
            return _customerRepository.GetQueryableCollection();
        }
    }
}
