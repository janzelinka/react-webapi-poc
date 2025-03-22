using System;
using System.Collections.Generic;
using api.Models.Events;
using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;

namespace api.Repositories
{
    public interface ICountryRepository : IBaseRepository<Country>
    {

    }
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApiDataContext context) : base(context)
        {
        }
    }
}