using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Service.DAL.Models
{
    public interface ITrackable
    {
        DateTime? CreatedAt { get; set; }

        //If we were to implement the user entity
        //int? CreatedBy { get; set; }
        DateTime? LastUpdatedAt { get; set; }

        //If we were to implement the user entity
        //int? LastUpdatedBy { get; set; }

        //I'm still considering if I'm going to do soft deletes for this example
        bool IsDeleted { get; set; }

    }
}
