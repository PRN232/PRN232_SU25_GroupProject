﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class Allergy
    {
        public int Id { get; set; }
        public int MedicalProfileId { get; set; }
        public string AllergyName { get; set; }
        public string Severity { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
    }

}
