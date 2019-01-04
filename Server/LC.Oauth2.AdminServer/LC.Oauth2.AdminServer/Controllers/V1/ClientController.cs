using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Oauth2.AdminServer.Controllers.Base;
using LC.Oauth2.AdminServer.Functions.Response;
using LC.Oauth2.AdminServer.ViewModel;
using LC.Oauth2.DbContext;
using LC.Oauth2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LC.Oauth2.AdminServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : LcControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly Oauth2DbContext _DbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public ClientController(Oauth2DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        /// <summary>
        /// Get client by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all client list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get client list
        /// </summary>
        /// <param name="keyWord">search key word</param>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size ,default is 20 </param>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> Get([FromQuery]string keyWord = "", [FromQuery] int page = 1, [FromQuery]int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create client
        /// </summary>
        /// <param name="vm">Client view model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientViewModel vm)
        {
            if (vm == null)
            {
                return await ResponseProvider.ParameterErrorResponse("Request parameter is null");
            }

            if (string.IsNullOrEmpty(vm.ClientName))
            {
                return await ResponseProvider.ParameterErrorResponse($"Parameter's {nameof(vm.ClientName)} is null or empty");
            }

            //checke client name
            if (await _DbContext.Clients.AnyAsync(m => m.ClientName == vm.ClientName))
            {
                return await ResponseProvider.ResourceExistResponse($"Parameter's {nameof(vm.ClientName)} is exist");
            }

            var client = new Client()
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = vm.ClientName,
                Enabled = vm.Enabled,
                AllowOfflineAccess = vm.AllowOfflineAccess,
                Created = DateTime.Now
            };

            var result = await _DbContext.AddAsync(client);
            await _DbContext.SaveChangesAsync();

            //update vm data
            vm.ClientId = result.Entity.ClientId;
            vm.Id = result.Entity.Id;

            return await ResponseProvider.OkResult(vm,"create success");
        }

        /// <summary>
        /// Update client
        /// </summary>
        /// <param name="vm">Client view model</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClientViewModel vm)
        {
            if (vm == null)
            {
                return await ResponseProvider.ParameterErrorResponse("Request parameter is null");
            }

            if (string.IsNullOrEmpty(vm.ClientName))
            {
                return await ResponseProvider.ParameterErrorResponse($"Parameter's {nameof(vm.ClientName)} is null or empty");
            }

            //checke client name
            if (await _DbContext.Clients.AnyAsync(m => m.ClientName == vm.ClientName && m.Id != vm.Id))
            {
                return await ResponseProvider.ResourceExistResponse($"Parameter's {nameof(vm.ClientName)} is exist");
            }

            var client = await _DbContext.Clients.FirstOrDefaultAsync(m => m.Id == vm.Id);

            //resouce is not found
            if (client == null)
            {
                return await ResponseProvider.ResourceNotFoundResponse();
            }

            //update property
            client.ClientName = vm.ClientName;
            client.AllowOfflineAccess = vm.AllowOfflineAccess;
            client.Enabled = vm.Enabled;
            client.Updated = DateTime.Now;

            var result = _DbContext.Update(client);
            await _DbContext.SaveChangesAsync();

            return await ResponseProvider.OkResult(vm,"update success");
        }

        /// <summary>
        /// 删除Client资源
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}