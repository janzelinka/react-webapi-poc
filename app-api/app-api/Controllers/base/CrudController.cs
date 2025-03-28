using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using app.Services;
using app_api.Models.Interfaces;

namespace api.Controllers
{
    public class GenericResult<T> {
        public Dictionary<string, string[]?> ? Errors { get; set;} = new Dictionary<string, string[]?>();
        public T? Value { get;set; }
    }

    [ApiController]
    public abstract class CrudController<V, S> : BaseController where S : ICrudService<V>
    {
        public S ViewService { get; }
        public CrudController(S viewService)
        {
            ViewService = viewService;
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]/Create")]
        public virtual GenericResult<Guid> Create(V item)
        {
            if (!ModelState.IsValid) {

                return new GenericResult<Guid>() {
                    Errors = Validate()
                };
            }
            return new GenericResult<Guid>() {
                Value = ViewService.Create(item)
            };
        }

        [HttpGet]
        [Authorize]
        [Route("[controller]/GetAll")]        
        public virtual IEnumerable<V> GetAll()
        {
            return ViewService.GetAll();
        }

        [HttpPut]
        [Authorize]
        [Route("[controller]/Update")]
        public virtual GenericResult<Guid> Update(V item)
        {
            if (!ModelState.IsValid) {
                return new GenericResult<Guid>() {
                    Errors = Validate()
                };
            }
            return new GenericResult<Guid>() {
                Value = ViewService.Update(item)
            };
        }

        [HttpDelete]
        [Authorize]
        [Route("[controller]/Delete")]
        public virtual void Delete(V item)
        {
            ViewService.Delete(item);
        }
    }
}