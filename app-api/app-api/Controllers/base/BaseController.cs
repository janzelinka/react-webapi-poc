using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    public class BaseController : ControllerBase {
        protected Dictionary<string, string[]?> Validate() {
            if (ModelState == null) {
                return new Dictionary<string, string[]?>();
            }
            return ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors?.Select(e => e.ErrorMessage).ToArray()
            );
        }
    }
}