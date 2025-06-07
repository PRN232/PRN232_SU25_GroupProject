using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IBackupService
    {
        Task<bool> CreateBackupAsync(BackupRequest request);
        Task<bool> RestoreBackupAsync(RestoreBackupRequest request);
        Task<IEnumerable<BackupInfo>> GetBackupHistoryAsync();
        Task<bool> ScheduleBackupAsync(ScheduleBackupRequest request);
    }

}
