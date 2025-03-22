using System;
using System.Collections.Generic;
using api.Models.Events;
using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;

namespace api.Repositories
{
    public interface ICityRepository : IBaseRepository<City>
    {

    }
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(ApiDataContext context) : base(context)
        {
        }
    }
}