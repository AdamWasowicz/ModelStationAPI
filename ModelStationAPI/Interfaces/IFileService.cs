using ModelStationAPI.Models;
using System;
using System.Collections.Generic;

namespace ModelStationAPI.Interfaces
{
    public interface IFileService
    {
        bool Delete(int id, int userId);
        FileStorageDTO GetById(int id);
        List<FileStorageDTO> GetFilesByPostId(int postId);
        FileStorageDTO GetUserImage(int userId);
        int Upload(CreateFileStorageDTO dto);
        Byte[] GetFileByFileStorageId(int fileStorageId);
        Byte[] GetFileByByFileStorageName(string storageName);
    }
}