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
        Tuple<byte[], string> GetFileByFileStorageId(int fileStorageId);
        Tuple<byte[], string> GetFileByFileStorageName(string storageName);
        List<FileStorageDTO> GetFilesByPostId(int postId);
        FileStorageDTO GetUserImage_ReturnDTO(int userId);
        Tuple<byte[], string> GetUserImage_ReturnImage(int userId);
        int Upload(CreateFileStorageDTO dto, ClaimsPrincipal userClaims);
    }
}