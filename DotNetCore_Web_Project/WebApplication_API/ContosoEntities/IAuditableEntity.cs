using System;

namespace WebApplication_API.ContosoEntities
{
    public interface IAuditableEntity
    {
        DateTime? CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }
}