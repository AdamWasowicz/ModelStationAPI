using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;
using ModelStationAPI.Services;
using ModelStationAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;

namespace ModelStationAPI.Controllers
{
    [Route("api/v1/fileStorage")]
    [ApiController]
    public class FileStorageController : Controller
    {
        private readonly IFileService _fileService;


        public FileStorageController(IFileService fileService)
        {
            _fileService = fileService;
        }


        [HttpDelete("{id}")]
        [Authorize(Policy = "IsUser")]
        public ActionResult Delete([FromRoute] int id)
        {
            bool isDeleted = _fileService.Delete(id, User);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }


        [HttpGet("dto/{id}")]
        public ActionResult<FileStorageDTO> GetDTO_ById([FromRoute] int id)
        {
            var fileStorageDTO = _fileService.GetById(id);

            if (fileStorageDTO == null)
                return NotFound();

            return Ok(fileStorageDTO);
        }


        [HttpGet("file/name/{storagename}")]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"storageName"})]
        public ActionResult GetFileByStorageName([FromRoute] string storageName)
        {
            var result = _fileService.GetFileByFileStorageName(storageName);
            var file = result.Item1;
            var name = result.Item2.Split('.')[0];

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(result.Item2, out string type);

            return File(file, type, name);
        }


        [HttpGet("file/id/{id}")]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "id" })]
        public ActionResult GetFileByStorageId([FromRoute] int id)
        {
            var result = _fileService.GetFileByFileStorageId(id);
            var file = result.Item1;
            var name = result.Item2.Split('.')[0];

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(result.Item2, out string type);

            return File(file, type, name);
        }


        [HttpGet("file/userid/{id}")]
        public ActionResult GetUserImage([FromRoute] int id)
        {
            var result = _fileService.GetUserImage_ReturnImage(id);
            var file = result.Item1;
            var name = result.Item2.Split('.')[0];

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(result.Item2, out string type);

            return File(file, type, name);
        }


        [HttpGet("dtos/{postid}")]
        public ActionResult<List<FileStorageDTO>> GetFilesByPostId([FromRoute] int postid)
        {
            var result = _fileService.GetFilesByPostId(postid);
            return Ok(result);
        }


        [HttpGet("userimage/dto/{userid}")]
        public ActionResult<FileStorageDTO> GetUserImageDTO([FromRoute] int userid)
        {
            var result = _fileService.GetUserImage_ReturnDTO(userid);
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Policy = "IsUser")]
        public ActionResult Upload([FromForm] CreateFileStorageDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _fileService.Upload(dto, User);

            return Created(result.ToString(), null);
        }
    }
}
