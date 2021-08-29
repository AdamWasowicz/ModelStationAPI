using ModelStationAPI.Models;
using System.Collections.Generic;

namespace ModelStationAPI.Interfaces
{
    public interface IFileService
    {
        bool Delete(int id, int userId);
        byte[] GetById(int id);
        byte[] GetByUserImage(int userId);
        List<byte[]> GetFilesByPostId(int postId);
        string HashName(CreateFileStorageDTO dto);
        int Upload(CreateFileStorageDTO dto);
    }
}