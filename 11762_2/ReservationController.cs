using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _11762_2
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public ReservationController()
        {
            //NuGetController.
        }

        // GET: api/<ReservationController>
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            PackageSearchResource resource = await repository.GetResourceAsync<PackageSearchResource>();
            SearchFilter searchFilter = new SearchFilter(includePrerelease: true);

            var result = new List<string>();
            IEnumerable<IPackageSearchMetadata> results = await resource.SearchAsync(
                "packageID: newtonsoft.json",
                searchFilter,
                skip: 0,
                take: 20,
                logger,
                cancellationToken);

            foreach (IPackageSearchMetadata packageData in results)
            {
                result.Add($"Found package {packageData.Identity.Id} {packageData.Identity.Version}");
                result.Add($"PrefixReserved: {packageData.PrefixReserved}");
            }

            return result;
        }

        // GET api/<ReservationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReservationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReservationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
