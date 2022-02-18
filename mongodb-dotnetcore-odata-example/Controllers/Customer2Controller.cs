using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using mongodb_dotnetcore_odata_example.Models;
using mongodb_dotnetcore_odata_example.Repositories;

namespace mongodb_dotnetcore_odata_example.Controllers
{
    [Route("[controller]")]
    public class Customer2Controller : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public Customer2Controller(CustomerRepository customerRepository)
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
