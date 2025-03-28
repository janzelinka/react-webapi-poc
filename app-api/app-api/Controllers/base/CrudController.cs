using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using app.Services;

namespace ing_app_api.Controllers
{
    [ApiController]
    public abstract class CrudController<V, S> where S : ICrudService<V>
    {
        public S ViewService { get; }
        public CrudController(S viewService)
        {
            ViewService = viewService;
        }

        [HttpPost]
        [Route("[controller]/Create")]
        public virtual Guid Create(V item)
        {
            return ViewService.Create(item);
        }

        [HttpGet]
        [Route("[controller]/GetAll")]        
        public virtual IEnumerable<V> GetAll()
        {
            return ViewService.GetAll();
        }

        [HttpPut]
        [Route("[controller]/Update")]
        public virtual Guid Update(V item)
        {
            return ViewService.Update(item);
        }

        [HttpDelete]
        [Route("[controller]/Delete")]
        public virtual void Delete(V item)
        {
            ViewService.Delete(item);
        }
    }
}