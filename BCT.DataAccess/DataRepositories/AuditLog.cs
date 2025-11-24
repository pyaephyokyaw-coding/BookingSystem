using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.Data;

namespace BCT.DataAccess.DataRepositories;

public class AuditLog(BookingSystemDbContext dbContext)
{
    public List<AuditLogModel> GetAll() => dbContext.AuditLogs
        .OrderByDescending(x => x.LogDate)
        .ToList();

    public AuditLogModel GetById(int id) =>
        dbContext.AuditLogs.FirstOrDefault(x => x.AuditLogId == id);

    public void Add(AuditLogModel log)
    {
        dbContext.AuditLogs.Add(log);
        dbContext.SaveChanges();
    }

    public void Update(AuditLogModel log)
    {
        dbContext.AuditLogs.Update(log);
        dbContext.SaveChanges();
    }

    public void Delete(AuditLogModel log)
    {
        dbContext.AuditLogs.Remove(log);
        dbContext.SaveChanges();
    }
}
