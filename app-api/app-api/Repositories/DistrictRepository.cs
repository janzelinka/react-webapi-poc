using System;
using System.Collections.Generic;
using api.Models.Events;
using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;

namespace api.Repositories
{
    public interface IDistrictRepository : IBaseRepository<District>
    {

    }
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(ApiDataContext context) : base(context)
        {
        }
    }
}