using app.Models;
using appapi.Factories;
using Microsoft.AspNetCore.Mvc;

namespace appapi.Controllers.Base
{
    public class BaseEnumController<T> : ControllerBase where T : BaseImport
    {
        private string IndexName = string.Empty;
        public IElasticFactory ElasticFactory { get; }
        public BaseEnumController(string indexName, IElasticFactory elasticFactory)
        {
            IndexName = indexName;
            ElasticFactory = elasticFactory;
        }

        [HttpGet]
        [Route("[controller]/GetAll")]
        public IEnumerable<T> GetAll([FromQuery] string name)
        {
            var all = ElasticFactory.CreateElasticClient(IndexName).Search<T>(s => s
                .Query(q => q
                    .QueryString(qs => qs
                        .Fields(f => f.Field(ff => ff.Name))
                        .Query(string.IsNullOrEmpty(name) ? "*" : $"{name}*")
                    )
                )
            );

            return all.Documents.ToList();
        }
    }
}