using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ModelStationAPI.Interfaces
{
    public interface IFileService
    {
        bool Delete(int id, ClaimsPrincipal userClaims);
        FileStorageDTO GetById(int id);
        Tuple<byte[], string> GetFileByFileStorageName(string storageName);
        Tuple<byte[], string> GetFileByFileStorageId(int fileStorageId);
        List<FileStorageDTO> GetFilesByPostId(int postId);
        FileStorageDTO GetUserImage(int userId);
        int Upload(CreateFileStorageDTO dto, ClaimsPrincipal userClaims);
    }
}