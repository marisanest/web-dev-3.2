using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Dev3.Models
{
    public class TodoModel
    {
        [DisplayName("Titel"), Required]
        public virtual String Titel { get; set; }
        [DisplayName("Beschreibung"), Required]
        public virtual string Description { get; set; }

        public virtual int Id { get; set; }

        public virtual ApplicationUser Owner { get; set; }

    }
}