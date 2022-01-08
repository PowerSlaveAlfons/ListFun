/*
 * ListFun API
 *
 * This API will allow my frontends to communicate with my backends. Should be the same in every implementation, but I'm just testing right now anyway.
 *
 * OpenAPI spec version: 0.1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using IO.Swagger.Attributes;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using IO.Swagger.Data;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DefaultApiController : ControllerBase
    {
        private readonly DBContext dbContext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public DefaultApiController(DBContext context)
        {
            dbContext = context;
        }
        
        /// <summary>
        /// Adds an Entry
        /// </summary>
        /// <remarks>Adds an entry to an existing Game or Movie.</remarks>
        /// <param name="body"></param>
        /// <response code="200">200 OK.</response>
        [HttpPost]
        [Route("/entries/add")]
        [ValidateModelState]
        [SwaggerOperation("AddEntry")]
        public virtual IActionResult AddEntry([FromBody]Entry body)
        {
            var searchResult = from m in dbContext.Media
                       where m.Id == body.Mediaid
                        select m;

            if (searchResult.Count() < 1)
                return StatusCode(400);

            dbContext.Update(body);
            dbContext.SaveChanges();
            return StatusCode(200);
        }

        /// <summary>
        /// Adds a Piece of Media
        /// </summary>
        /// <remarks>Adds piece of Media</remarks>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/media/add")]
        [ValidateModelState]
        [SwaggerOperation("AddMedia")]
        public virtual IActionResult AddMedia([FromBody]Media body)
        {
            if (ModelState.IsValid)
            {
                dbContext.Update(body);
                dbContext.SaveChanges();

                return StatusCode(200);
            }

            Console.WriteLine(body.ToString());
            return StatusCode(400);
        }

        /// <summary>
        /// gets all Entries
        /// </summary>
        /// <remarks>Gets every game or movie that has been completed along with the time.</remarks>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/entries/getAll")]
        [ValidateModelState]
        [SwaggerOperation("GetAllEntries")]
        public virtual IActionResult GetAllEntries()
        {
            var entries = from e in dbContext.Entry
                          select e;

            if (entries.Count() < 1)
                return StatusCode(400);

            return new ObjectResult(entries);
        }

        /// <summary>
        /// Gets All Games
        /// </summary>
        /// <remarks>Gets all the games.</remarks>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/media/getAll")]
        [ValidateModelState]
        [SwaggerOperation("GetAllMedia")]
        [SwaggerResponse(statusCode: 200, type: typeof(Media), description: "")]
        public virtual IActionResult GetAllMedia()
        {
            var Media = from m in dbContext.Media
                        select m;
            return new ObjectResult(Media);
        }


        /// <summary>
        /// Gets all Media of a specific Type
        /// </summary>
        /// <param name="Type">The Type of the Media to be returned</param>
        /// <remarks>ets all Media of a specific Type</remarks>
        [HttpGet]
        [Route("/media/getAllOfType/{type}")]
        [ValidateModelState]
        [SwaggerOperation("GetAllOfType")]
        public virtual IActionResult GetAllOfType(string Type)
        {
            var Media = from m in dbContext.Media
                        where m.Type == Type
                        select m;

            if (Media.Count() < 1)
                return StatusCode(400);
            
            return new ObjectResult(Media);

        }

        /// <summary>
        /// Gets all Entries for a single piece of media.
        /// </summary>
        /// <remarks>Gets all Entries for a single piece of media.</remarks>
        [HttpGet]
        [Route("/entries/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetAllEntriesPerMedia")]
        public virtual IActionResult GetAllEntriesPerGame(int id)
        {
            var entries = from e in dbContext.Entry
                          where e.Mediaid == id
                          select e;

            if (entries.Count() < 1)
                return StatusCode(400);
          
            return new ObjectResult(entries);
        }

        /// <summary>
        /// Gets Infos for a specific piece of Media
        /// </summary>
        /// <param name="id">The id of the piece of Media</param>
        /// <remarks>Gets Infos for a specific piece of Media</remarks>
        [HttpGet]
        [Route("media/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetMedia")]
        public virtual IActionResult GetMedia(int id)
        {
            var Media = from m in dbContext.Media
                        where m.Id == id
                        select m;

            if (Media.Count() < 1)
                return StatusCode(400);

            return new ObjectResult(Media.FirstOrDefault());
        }

        /// <summary>
        /// Updates the TimeStamp of a specific Entry. Needs "".
        /// </summary>
        [HttpPost]
        [Route("entry/updateTimeStamp/{id}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateTimeStamp")]
        public virtual IActionResult UpdateTimeStamp(int id, [FromBody]string body)
        {
            var Entry = from e in dbContext.Entry
                        where e.ID == id
                        select e;

            if (Entry.Count() < 1)
                return StatusCode(400);

            Entry.FirstOrDefault().Timestamp = DateTime.Parse(body);

            dbContext.SaveChanges();
            return StatusCode(200);

        }
    }
}
