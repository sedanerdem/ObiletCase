using ObiletCase.Models;
using ObiletCase.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletCase.Interface
{
    public interface ICallApiService
    {
        Task<TResponse> CallApi<TRequest, TResponse>(string url, TRequest bodyObject);
    }
}
