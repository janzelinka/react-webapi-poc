using System;
using System.Collections.Generic;
using api.Models.Events;
using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;

namespace api.Repositories
{
    public interface IStateRepository : IBaseRepository<State>
    {

    }
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(ApiDataContext context) : base(context)
        {
        }
    }
}