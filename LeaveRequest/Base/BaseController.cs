using LeaveRequest.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LeaveRequest.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                var result = repository.Insert(entity);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(405);
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<Entity> entities = repository.Get();
            return Ok(entities);
        }

        //[HttpGet]
        //public async Task<ActionResult> Get()
        //{
        //    IEnumerable<Entity> entities = await Task.Run(() => repository.Get());
        //    return Ok(entities);
        //}

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            Entity entities = repository.Get(key);
            return Ok(entities);
        }
        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            Entity entities = repository.Get(key);
            if (entities != null)
            {
                var result = repository.Delete(key);
                return Ok(result);
            }
            else
            {
                return NotFound($"Data {key} tidak ditemukan");
            }
            //try
            //{
            //    return Ok(repository.Delete(key));
            //}
            //catch (Exception)
            //{
            //    return NotFound("Data tidak ditemukan");
            //}
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                var result = repository.Update(entity);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(405, new { status = HttpStatusCode.NotModified, message = "test" });
                //return NotFound();
            }
        }
    }
}
