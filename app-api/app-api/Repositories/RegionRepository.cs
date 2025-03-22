using System;
using System.Collections.Generic;
using api.Models.Events;
using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;

namespace api.Repositories
{
    public interface IRegionRepository : IBaseRepository<Region>
    {

    }
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(ApiDataContext context) : base(context)
        {
        }
    }
}