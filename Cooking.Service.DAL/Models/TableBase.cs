using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Service.DAL.Models
{
    /// <summary>
    /// This is overkill I know but it demonstrates what a bigger model might need for production purposes
    /// </summary>
    public class TableBase : ITrackable
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
