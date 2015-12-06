using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rad301_ca2_S00140633.Models
{
    public enum Genders {Male,Female}
    public class Actor
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Genders Gender { get; set; }

        [Display(Name = "Name"), Required]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }
        public string Character { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}