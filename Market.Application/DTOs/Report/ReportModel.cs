using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.DTOs.Report
{
    public class ReportModel
    {
        public Guid? ProductId { get; set; }
        public Guid? OrganizationOrCustomerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
